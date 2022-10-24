using System.Web.Http;
using WebActivatorEx;
using SomeonesToDoListApp;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace SomeonesToDoListApp
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c => { c.SingleApiVersion("v1", "ToDoListApp"); })
                .EnableSwaggerUi();
        }
    }
}