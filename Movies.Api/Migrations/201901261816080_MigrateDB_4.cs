namespace Movies.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB_4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RawDatas", "CreateAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.RawDatas", "UpdateAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RawDatas", "UpdateAt");
            DropColumn("dbo.RawDatas", "CreateAt");
        }
    }
}
