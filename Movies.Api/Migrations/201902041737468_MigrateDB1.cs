namespace Movies.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MovieFromPosts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PostId = c.Int(nullable: false),
                        FromId = c.Int(nullable: false),
                        Text = c.String(),
                        Photos = c.String(),
                        Videos = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MovieFromPosts");
        }
    }
}
