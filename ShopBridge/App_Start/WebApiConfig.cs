using FluentValidation.WebApi;
using ShopBridge.App_Start;
using ShopBridge.ExceptionFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ShopBridge
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Adding Filter for Validating the Model
            config.Filters.Add(new ValidateProductStateFilter());
            FluentValidationModelValidatorProvider.Configure(config);

            //Adding Filter for Custom Exception
            config.Filters.Add(new CustomExceptionFilter());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
