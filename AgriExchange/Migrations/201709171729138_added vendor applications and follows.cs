namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedvendorapplicationsandfollows : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FollowedUser_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowedUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.FollowedUser_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.VendorApplications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Follows", "FollowedUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Follows", new[] { "User_Id" });
            DropIndex("dbo.Follows", new[] { "FollowedUser_Id" });
            DropTable("dbo.VendorApplications");
            DropTable("dbo.Follows");
        }
    }
}
