﻿using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Data.Repositories.Logic;
using Movies.Api.Models;
using Movies.Api.Services.Interfaces;
using Movies.Api.Services.Logic;
using System.Web.Http;
using Unity;

namespace Movies.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IMovieRepository, MovieRepository>();
            container.RegisterType<IMovieManager, MovieManager>();
            config.DependencyResolver = new UnityResolver(container);

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
