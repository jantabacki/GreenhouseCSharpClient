using GreenhouseUIClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenhouseUIClient.Model
{
    public class Entity : BaseEntity
    {
        private ushort objectId;
        public ushort ObjectId { get { return objectId; } private set { objectId = value; OnPropertyChanged(); } }

        private byte humidity;
        public byte Humidity { get { return humidity; } private set { humidity = value; OnPropertyChanged(); } }

        private byte temperature;
        public byte Temperature { get { return temperature; } private set { temperature = value; OnPropertyChanged(); } }

        private byte insolation;
        public byte Insolation { get { return insolation; } private set { insolation = value; OnPropertyChanged(); } }

        private string colorStateString = "White";
        public string ColorStateString
        {
            get
            {
                return colorStateString;
            }
            private set
            {
                colorStateString = value;
                OnPropertyChanged();
            }
        }

        public bool IsObjectUpdated
        {
            get
            {
                return (DateTime.Now - lastUpdate).TotalSeconds < 30;
            }
        }

        private DateTime lastUpdate;

        public Entity(ushort objectId)
        {
            this.ObjectId = objectId;
            Humidity = 0;
            Temperature = 0;
            Insolation = 0;
        }

        public void SetEntityValidState(bool state)
        {
            if (state)
            {
                if ((DateTime.Now - lastUpdate).TotalSeconds < 30)
                {
                    ColorStateString = "YellowGreen";
                }
                else
                {
                    ColorStateString = "LightGray";
                }
            }
            else
            {
                ColorStateString = "White";
            }
        }

        public Entity UpdateState(byte humidity, byte temperature, byte insolation)
        {
            this.Humidity = humidity;
            this.Temperature = temperature;
            this.Insolation = insolation;
            lastUpdate = DateTime.Now;
            OnPropertyChanged();
            return this;
        }
    }
}
