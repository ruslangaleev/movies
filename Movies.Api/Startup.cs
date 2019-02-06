using Hangfire;
using Microsoft.Owin;
using Movies.Api.Services.Interfaces;
using Movies.Api.Services.Logic;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using GlobalConfiguration = Hangfire.GlobalConfiguration;

[assembly: OwinStartup(typeof(Movies.Api.Startup))]
namespace Movies.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("MovieContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<IParser>(t => t.StartParserNewPosts(), Cron.MinuteInterval(10));
            RecurringJob.AddOrUpdate<IParser>(t => t.StartParserAllPosts(), Cron.MinuteInterval(10));
        }
    }
}