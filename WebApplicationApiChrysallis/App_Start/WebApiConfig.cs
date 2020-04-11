using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplicationApiChrysallis
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Control de referencias circulares a objetos
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
