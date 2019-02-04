using Movies.Api.Models;
using System.Data.Entity;

namespace Movies.Api.Data
{
    public class MovieContext : DbContext
    {
        public DbSet<MovieFromPost> MoviesFromPosts { get; set; }

        public MovieContext() : base("name=MovieContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<MovieContext>(null);
            //base.OnModelCreating(modelBuilder);
        }
    }
}