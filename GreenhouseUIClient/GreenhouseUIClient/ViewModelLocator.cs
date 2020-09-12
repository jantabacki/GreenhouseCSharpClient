using Autofac;
using GreenhouseUIClient.Model;
using GreenhouseUIClient.Services;
using GreenhouseUIClient.ViewModel;

namespace GreenhouseUIClient
{
    public class ViewModelLocator
    {
        private IContainer container;
        public EntitiesViewModel EntitesViewModel => container.Resolve<EntitiesViewModel>();
        public ViewModelLocator()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<EntitiesViewModel>().SingleInstance();
            containerBuilder.RegisterType<TcpEntityService>().As<IEntityService<Entity>>();
            container = containerBuilder.Build();
        }
    }
}
