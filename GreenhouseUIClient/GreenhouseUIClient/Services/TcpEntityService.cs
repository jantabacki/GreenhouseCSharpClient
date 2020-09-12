using GreenhouseInterface;
using GreenhouseUIClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace GreenhouseUIClient.Services
{
    class TcpEntityService : IEntityService<Entity>
    {
        private List<Entity> entites = new List<Entity>();
        private Timer updateTimer = new Timer();
        int port;
        string address;

        public TcpEntityService()
        {
            var appConfig = ConfigurationManager.AppSettings;
            updateTimer.Interval = double.Parse(appConfig["updateInterval"]);
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            updateTimer.Enabled = true;
            port = int.Parse(appConfig["greenhouseInterfacePort"]);
            address = appConfig["greenhouseInterfaceAddress"];
            string objectDefinitionsFileContent = File.ReadAllText(appConfig["objectDefinitionsFile"]);
            entites = JsonConvert.DeserializeObject<List<Entity>>(objectDefinitionsFileContent);
        }

        private void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateState();
        }

        private void SendData(byte[] data)
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(address, port);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(data, 0, data.Length);
            networkStream.Close();
            tcpClient.Close();
        }

        private byte[] SendDataWithResponse(byte[] data)
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(address, port);
            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(data, 0, data.Length);
            byte[] buffer = new byte[2 * 1024];
            networkStream.Read(buffer, 0, buffer.Length);
            networkStream.Close();
            tcpClient.Close();
            return buffer;
        }

        public void ChangeStateCommand(Entity entity)
        {
            Command command = new Command(CommandType.SetParameters, entity.ObjectId, entity.Humidity, entity.Temperature, entity.Insolation);
            string jsonData = JsonConvert.SerializeObject(command);
            byte[] buffer = Encoding.ASCII.GetBytes(jsonData);
            SendData(buffer);
        }

        public IEnumerable<Entity> Get()
        {
            return entites;
        }

        public void UpdateState()
        {
            try
            {
                Command command = new Command(CommandType.GetData, 0, 0, 0, 0);
                string jsonData = JsonConvert.SerializeObject(command);
                byte[] buffer = Encoding.ASCII.GetBytes(jsonData);
                byte[] response = SendDataWithResponse(buffer);
                string jsonDataResponse = Encoding.ASCII.GetString(response, 0, response.Length);
                List<Entity> receivedEntites = JsonConvert.DeserializeObject<List<Entity>>(jsonDataResponse);
                foreach (Entity receivedEntity in receivedEntites)
                {
                    Entity entity = entites.FirstOrDefault(e => e.ObjectId.Equals(receivedEntity.ObjectId));
                    if (entity != null)
                    {
                        entity.UpdateState(receivedEntity.Humidity, receivedEntity.Temperature, receivedEntity.Insolation, receivedEntity.IsObjectUpdated);
                        entity.SetEntityValidState(true);
                    }
                }
            }
            catch (Exception e)
            {
                foreach (Entity entity in entites)
                {
                    entity.SetEntityValidState(false);
                }
            }
        }
    }
}
