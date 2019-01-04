namespace Movies.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieContents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quality = c.Int(nullable: false),
                        Url = c.String(),
                        MovieInfo_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MovieInfoes", t => t.MovieInfo_Id)
                .Index(t => t.MovieInfo_Id);
            
            CreateTable(
                "dbo.MovieInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        UrlPoster = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieContents", "MovieInfo_Id", "dbo.MovieInfoes");
            DropIndex("dbo.MovieContents", new[] { "MovieInfo_Id" });
            DropTable("dbo.MovieInfoes");
            DropTable("dbo.MovieContents");
        }
    }
}
