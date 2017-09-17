namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedaddressbadgeandcropentrytables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        addressLine = c.String(),
                        City_ID = c.Int(),
                        Zip_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.City_ID)
                .ForeignKey("dbo.ZipCodes", t => t.Zip_ID)
                .Index(t => t.City_ID)
                .Index(t => t.Zip_ID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        State_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.States", t => t.State_ID)
                .Index(t => t.State_ID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        State = c.String(),
                        abbreviation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        zip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Badges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CropEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Crop = c.String(),
                        DatePlanted = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CropEntries", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "Zip_ID", "dbo.ZipCodes");
            DropForeignKey("dbo.Addresses", "City_ID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "State_ID", "dbo.States");
            DropIndex("dbo.CropEntries", new[] { "User_Id" });
            DropIndex("dbo.Cities", new[] { "State_ID" });
            DropIndex("dbo.Addresses", new[] { "Zip_ID" });
            DropIndex("dbo.Addresses", new[] { "City_ID" });
            DropTable("dbo.CropEntries");
            DropTable("dbo.Badges");
            DropTable("dbo.ZipCodes");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
        }
    }
}
