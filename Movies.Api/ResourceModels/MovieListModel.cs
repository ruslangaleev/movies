using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.ResourceModels
{
    public class MovieListModel
    {
        public IEnumerable<AddMovieInfo> Movies { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}