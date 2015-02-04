using Exchange.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Exchange
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter); //remove xml format


            //custom json datetime 
            JsonMediaTypeFormatter jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
            jSettings.Converters.Add(new MyDateTimeConverter());
            jsonFormatter.SerializerSettings = jSettings;

        }
    }
}
