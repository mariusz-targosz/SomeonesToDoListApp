using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using SomeonesToDoListApp.Services.Mappers;

namespace SomeonesToDoListApp
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfiguration.Initialize();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
