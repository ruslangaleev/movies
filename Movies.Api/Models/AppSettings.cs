using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.Models
{
    public class AppSettings
    {
        public Parser Parser { get; set; }
    }

    public class Parser
    {
        public int Offset { get; set; }
    }
}