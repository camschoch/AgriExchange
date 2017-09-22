namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlockedUntil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "BlockedUntil", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "BlockedUntil");
        }
    }
}
