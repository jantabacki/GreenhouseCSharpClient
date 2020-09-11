using GreenhouseUIClient.Model;
using GreenhouseUIClient.Services;
using GreenhouseUIClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenhouseUIClient.ViewModel
{
    public class EntitiesViewModel : BaseViewModel
    {
        public IEnumerable<Entity> Entities { get; private set; }
        private readonly IEntityService<Entity> entityService;
        public EntitiesViewModel(IEntityService<Entity> entityService)
        {
            this.entityService = entityService;
            Load();
        }

        private void Load()
        {
            Entities = entityService.Get();
        }
    }
}
