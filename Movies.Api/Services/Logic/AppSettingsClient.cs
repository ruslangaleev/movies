using Movies.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Movies.Api.Services.Logic
{
    public class AppSettingsClient
    {
        public AppSettings Get()
        {
            try
            {
                string text = File.ReadAllText("appsettings.json");
                return JsonConvert.DeserializeObject<AppSettings>(text);
            }
            catch(Exception e)
            {
                var s = e;
            }

            return null;
        }

        public void Set(AppSettings settings)
        {
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(settings));
        }
    }
}