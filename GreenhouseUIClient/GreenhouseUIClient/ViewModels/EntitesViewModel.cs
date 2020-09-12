using GreenhouseUIClient.Model;
using GreenhouseUIClient.Services;
using GreenhouseUIClient.ViewModels;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GreenhouseUIClient.ViewModel
{
    public class EntitiesViewModel : BaseViewModel
    {
        public IEnumerable<Entity> Entities { get; private set; }

        private readonly IEntityService<Entity> entityService;

        private Entity selectedObject;
        public Entity SelectedObject
        {
            get
            {
                return selectedObject;
            }
            set
            {
                selectedObject = value; OnPropertyChanged(this);
            }
        }

        public EntitiesViewModel(IEntityService<Entity> entityService)
        {
            this.entityService = entityService;
            Load();
        }

        private void Load()
        {
            Entities = entityService.Get();
        }

        public ICommand EntityClickCommand { get { return new DelegateCommand<Entity>(EntityClick); } }

        private void EntityClick(Entity entity)
        {
            if (selectedObject == entity)
            {
                entity.UnmarkSelected();
                selectedObject = null;
            }
            else
            {
                if (selectedObject == null)
                {
                    entity.MarkSelected();
                }
                else
                {
                    selectedObject.UnmarkSelected();
                    entity.MarkSelected();
                }
                selectedObject = entity;
            }
        }

        public ICommand ChangeStateCommand { get { return new DelegateCommand<string>(ChangeState); } }

        private void ChangeState(string value)
        {
            if (selectedObject != null)
            {
                Entity updateEntity = new Entity(selectedObject.ObjectId);
                updateEntity.UpdateState(selectedObject.Humidity, selectedObject.Temperature, selectedObject.Insolation, selectedObject.IsObjectUpdated);
                switch (value)
                {
                    case "IncreaseHumidity":
                        if (updateEntity.Humidity < byte.MaxValue)
                        {
                            updateEntity.Humidity += 1;
                        }
                        break;
                    case "DecreaseHumidity":
                        if (updateEntity.Humidity > byte.MinValue)
                        {
                            updateEntity.Humidity -= 1;
                        }
                        break;
                    case "IncreaseTemperature":
                        if (updateEntity.Temperature < byte.MaxValue)
                        {
                            updateEntity.Temperature += 1;
                        }
                        break;
                    case "DecreaseTemperature":
                        if (updateEntity.Temperature > byte.MinValue)
                        {
                            updateEntity.Temperature -= 1;
                        }
                        break;
                    case "IncreaseInsolation":
                        if (updateEntity.Insolation < byte.MaxValue)
                        {
                            updateEntity.Insolation += 1;
                        }
                        break;
                    case "DecreaseInsolation":
                        if (updateEntity.Insolation > byte.MinValue)
                        {
                            updateEntity.Insolation -= 1;
                        }
                        break;
                    default:
                        return;
                }
                entityService.ChangeStateCommand(updateEntity);
            }
        }
    }
}
