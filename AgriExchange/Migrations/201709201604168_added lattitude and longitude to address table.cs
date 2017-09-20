namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlattitudeandlongitudetoaddresstable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Lattitude", c => c.Single(nullable: false));
            AddColumn("dbo.Addresses", "Longitude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "Longitude");
            DropColumn("dbo.Addresses", "Lattitude");
        }
    }
}
