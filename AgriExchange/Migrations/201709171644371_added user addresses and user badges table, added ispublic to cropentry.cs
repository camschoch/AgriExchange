namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduseraddressesanduserbadgestableaddedispublictocropentry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Address_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Addresses", t => t.Address_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Address_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserBadges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateEarned = c.DateTime(nullable: false),
                        Badge_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Badges", t => t.Badge_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Badge_ID)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.CropEntries", "IsPublic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBadges", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserBadges", "Badge_ID", "dbo.Badges");
            DropForeignKey("dbo.UserAddresses", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserAddresses", "Address_ID", "dbo.Addresses");
            DropIndex("dbo.UserBadges", new[] { "User_Id" });
            DropIndex("dbo.UserBadges", new[] { "Badge_ID" });
            DropIndex("dbo.UserAddresses", new[] { "User_Id" });
            DropIndex("dbo.UserAddresses", new[] { "Address_ID" });
            DropColumn("dbo.CropEntries", "IsPublic");
            DropTable("dbo.UserBadges");
            DropTable("dbo.UserAddresses");
        }
    }
}
