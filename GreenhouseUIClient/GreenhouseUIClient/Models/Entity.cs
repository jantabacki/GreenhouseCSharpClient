using GreenhouseUIClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenhouseUIClient.Model
{
    public class Entity : Base
    {
        private ushort objectId;
        public ushort ObjectId { get { return objectId; } set { objectId = value; OnPropertyChanged(); } }

        private byte humidity;
        public byte Humidity { get { return humidity; } set { humidity = value; OnPropertyChanged(); } }

        private byte temperature;
        public byte Temperature { get { return temperature; } set { temperature = value; OnPropertyChanged(); } }

        private byte insolation;
        public byte Insolation { get { return insolation; } set { insolation = value; OnPropertyChanged(); } }

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

        private bool isObjectUpdated = false;
        public bool IsObjectUpdated
        {
            get
            {
                return isObjectUpdated;
            }
            set
            {
                isObjectUpdated = value;
                OnPropertyChanged();
            }
        }

        public Entity(ushort objectId)
        {
            ObjectId = objectId;
            Humidity = 0;
            Temperature = 0;
            Insolation = 0;
        }

        public void SetEntityValidState(bool state)
        {
            if (state)
            {
                if (IsObjectUpdated)
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

        public Entity UpdateState(byte humidity, byte temperature, byte insolation, bool isObjectUpdated)
        {
            Humidity = humidity;
            Temperature = temperature;
            Insolation = insolation;
            IsObjectUpdated = isObjectUpdated;
            return this;
        }
    }
}
