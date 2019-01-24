namespace Movies.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieSources",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Quality = c.Int(nullable: false),
                        Url = c.String(),
                        CreateAt = c.DateTime(nullable: false),
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
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieSources", "MovieInfo_Id", "dbo.MovieInfoes");
            DropIndex("dbo.MovieSources", new[] { "MovieInfo_Id" });
            DropTable("dbo.MovieInfoes");
            DropTable("dbo.MovieSources");
        }
    }
}
