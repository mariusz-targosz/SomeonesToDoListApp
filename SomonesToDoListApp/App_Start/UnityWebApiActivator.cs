using System.Web.Http;
using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SomeonesToDoListApp.UnityWebApiActivator), nameof(SomeonesToDoListApp.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SomeonesToDoListApp.UnityWebApiActivator), nameof(SomeonesToDoListApp.UnityWebApiActivator.Shutdown))]

namespace SomeonesToDoListApp
{
    public static class UnityWebApiActivator
    {
        public static void Start() 
        {
            var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.Container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}