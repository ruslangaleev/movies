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

        public DbSet<MovieSource> MovieContents { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<RawData> RawDatas { get; set; }

        public MovieContext() : base("name=MovieContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<MovieContext>(null);
            //base.OnModelCreating(modelBuilder);
        }
    }
}