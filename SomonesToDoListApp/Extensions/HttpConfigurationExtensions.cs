using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace SomeonesToDoListApp.Extensions
{
    public static class HttpConfigurationExtensions
    {
        public static void SetupJsonFormatter(this HttpConfiguration httpConfiguration)
        {
            var formatters = httpConfiguration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}