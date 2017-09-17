namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedratingstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Rate = c.Int(nullable: false),
                        RatingUser_Id = c.String(maxLength: 128),
                        UserRated_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.RatingUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserRated_Id)
                .Index(t => t.RatingUser_Id)
                .Index(t => t.UserRated_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ratings", "UserRated_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ratings", "RatingUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ratings", new[] { "UserRated_Id" });
            DropIndex("dbo.Ratings", new[] { "RatingUser_Id" });
            DropTable("dbo.Ratings");
        }
    }
}
