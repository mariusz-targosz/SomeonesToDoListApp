using SomeonesToDoListApp;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace SomeonesToDoListApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
        }
    }
}
