namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedzonetoaddresstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Zone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "Zone");
        }
    }
}
