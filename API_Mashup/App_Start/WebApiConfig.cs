using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using System.Web.Http;
using Microsoft.Web.Http;

namespace API_Mashup
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Setup API Versioning
            config.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1,0);
            });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/v{apiversion}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
