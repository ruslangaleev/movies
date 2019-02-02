using Hangfire;
using Movies.Api.Services.Interfaces;
using Movies.Api.Services.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Movies.Api.Controllers
{
    [RoutePrefix("api/dispatcher")]
    public class DispatcherController : ApiController
    {
        private readonly IParser _parser;

        public DispatcherController(IParser parser)
        {
            _parser = parser;
        }

        [Route("parser/start")]
        [HttpGet]
        public async Task GetInfo()
        {
            //BackgroundJob.Schedule<IParser>(t => t.Start(), TimeSpan.FromMinutes(1));
            //RecurringJob.AddOrUpdate<IParser>(t => t.Start(), Cron.Minutely);
            //RecurringJob.AddOrUpdate(() => _parser.Start(), Cron.Minutely);
            
            //var isEnabledParser = _parser.IsEnabledParser;

            //return new
            //{
            //    isEnabledParser
            //};
        }
    }
}