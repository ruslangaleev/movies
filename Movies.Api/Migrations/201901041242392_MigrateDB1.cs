namespace Movies.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieContents", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MovieInfoes", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.MovieInfoes", "UpdateAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieInfoes", "UpdateAt");
            DropColumn("dbo.MovieInfoes", "CreateAt");
            DropColumn("dbo.MovieContents", "CreateAt");
        }
    }
}
