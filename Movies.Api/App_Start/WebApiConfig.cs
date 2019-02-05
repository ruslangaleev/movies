using Hangfire;
using Movies.Api.Data;
using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Data.Repositories.Logic;
using Movies.Api.Models;
using Movies.Api.Services.Interfaces;
using Movies.Api.Services.Logic;
using System.Data.Entity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace Movies.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<DbContext, MovieContext>(new PerThreadLifetimeManager());

            container.RegisterType<IMovieFromPostRepository, MovieFromPostRepository>();
            container.RegisterType<IMovieFromPostManager, MovieFromPostManager>();

            container.RegisterType<IParser, Services.Logic.Parser>();

            container.RegisterType<IVkontakteClient, VkontakteClient>();

            container.RegisterType<IConfiguration, Configuration>();

            Hangfire.GlobalConfiguration.Configuration.UseActivator(new UnityJobActivator(container));
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
