using Movies.Api.Models;
using System.Collections.Generic;

namespace Movies.Api.ResourceModels
{
    public class MovieListModel
    {
        public IEnumerable<MovieFromPost> Movies { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}