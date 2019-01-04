using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Movies.Api.Data
{
    public class MovieContext : DbContext
    {
        public DbSet<MovieInfo> MovieInfos { get; set; }

        public DbSet<MovieContent> MovieContents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MovieInfo>().HasMany(c => c.MovieContents)
            //    //.WithMany(s => s.)
            //    .Map(t => t.MapLeftKey("CourseId")
            //    .MapRightKey("StudentId")
            //    .ToTable("CourseStudent"));
        }
    }
}