using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RecipeFinder.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Changes JSON output from Pascal case to Camel case.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //Changes JSON output from enum ordinal to enum name.
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            config.Routes.MapHttpRoute(
                name: "createUser",
                routeTemplate: "api/user/create",
                defaults: new { controller = "User", action = "Create" }
            );

            config.Routes.MapHttpRoute(
                name: "login",
                routeTemplate: "api/user/ValidLogin",
                defaults: new { controller = "User", action = "ValidLogin" }
            );

            config.Routes.MapHttpRoute(
                name: "secretArea",
                routeTemplate: "api/user/SecretArea",
                defaults: new { controller = "User", action = "SecretArea" }
            );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
