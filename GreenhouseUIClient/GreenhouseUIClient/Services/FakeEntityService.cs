using GreenhouseUIClient.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace GreenhouseUIClient.Services
{
    public class FakeEntityService : IEntityService<Entity>
    {
        private List<Entity> entites = new List<Entity>();

        private Timer fakeUpdateTimer = new Timer();

        public FakeEntityService()
        {
            entites.Add(new Entity(1).UpdateState(10, 20, 30));
            entites.Add(new Entity(2).UpdateState(40, 50, 60));
            entites.Add(new Entity(3).UpdateState(70, 80, 90));
            entites.Add(new Entity(4).UpdateState(100, 110, 120));

            fakeUpdateTimer.Interval = 1000;
            fakeUpdateTimer.Elapsed += FakeUpdateTimer_Elapsed;
            fakeUpdateTimer.Enabled = true;
        }

        private void FakeUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Random random = new Random(DateTime.Now.Second);
            foreach (Entity entity in entites)
            {
                entity.UpdateState((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
                entity.SetEntityValidState(random.Next(0, 2) == 1);
            }
        }

        public void ChangeStateCommand(Entity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entity> Get()
        {
            return entites;
        }

        public void UpdateState()
        {
            throw new NotImplementedException();
        }
    }
}
