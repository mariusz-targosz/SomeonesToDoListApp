using System;
using AutoMapper;
using SomeonesToDoListApp.DataAccessLayer.Context;
using SomeonesToDoListApp.DataAccessLayer.Repositories;
using SomeonesToDoListApp.Mappers;
using SomeonesToDoListApp.Services.Logging;
using SomeonesToDoListApp.Services.Services;
using Unity;
using Unity.Lifetime;

namespace SomeonesToDoListApp
{
    public static class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Container => _container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<SomeonesToDoListContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IToDoRepository, ToDoRepository>();
            container.RegisterType<IToDoFactory, ToDoFactory>();
            container.RegisterType<IDateTimeProvider, DateTimeProvider>();

            container.RegisterType(typeof(ILogger<>), typeof(NLogger<>));
            container.RegisterInstance(InitializeMapper());
        }

        private static IMapper InitializeMapper()
        {
            var autoMapperConfiguration = new MapperConfiguration(expression =>
            {
                expression.AddProfile<ToDoMappingProfile>();
            });
            return autoMapperConfiguration.CreateMapper();
        }
    }
}