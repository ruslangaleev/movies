namespace Movies.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB_3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RawDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Published = c.Boolean(nullable: false),
                        PostId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Text = c.String(),
                        Attachments = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RawDatas");
            DropTable("dbo.Accounts");
        }
    }
}
