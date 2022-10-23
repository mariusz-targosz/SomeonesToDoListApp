using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using SomeonesToDoListApp.Attributes;
using SomeonesToDoListApp.Extensions;

namespace SomeonesToDoListApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ValidateModelAttribute());
            config.MapHttpAttributeRoutes();
            config.SetupJsonFormatter();

            var frontUrl = ConfigurationManager.AppSettings["FrontUrl"];
            config.EnableCors(new EnableCorsAttribute(frontUrl, "*", "*"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
