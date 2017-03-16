using Log.OpenApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Log.OpenApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //未处理异常
            config.Filters.Add(new GlobalHandleErrorAttribute());
        }
    }
}
