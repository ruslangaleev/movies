using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.ResourceModels
{
    public class AddMovieContent
    {
        public Guid Id { get; set; }

        public MovieQuality Quality { get; set; }

        public string Url { get; set; }
    }
}