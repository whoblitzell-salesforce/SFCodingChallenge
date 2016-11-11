using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

namespace TwitterFeedReader
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            // JSON only...
            
            configuration.Formatters.Clear();
            configuration.Formatters.Add(new JsonMediaTypeFormatter());

            // Web API routes
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
