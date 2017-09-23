namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddatecheckedtofollowtableandaddeddateharvestedtocropentry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CropEntries", "DateHarvested", c => c.DateTime(nullable: false));
            AddColumn("dbo.Follows", "DateChecked", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Follows", "DateChecked");
            DropColumn("dbo.CropEntries", "DateHarvested");
        }
    }
}
