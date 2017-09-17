namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedblocksandreports : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BlockedUser_Id = c.String(maxLength: 128),
                        BlockingUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.BlockedUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BlockingUser_Id)
                .Index(t => t.BlockedUser_Id)
                .Index(t => t.BlockingUser_Id);
            
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Reason = c.String(),
                        ReportedBlogPost_ID = c.Int(),
                        ReportedComment_ID = c.Int(),
                        ReportingUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogPosts", t => t.ReportedBlogPost_ID)
                .ForeignKey("dbo.Comments", t => t.ReportedComment_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ReportingUser_Id)
                .Index(t => t.ReportedBlogPost_ID)
                .Index(t => t.ReportedComment_ID)
                .Index(t => t.ReportingUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "ReportingUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reports", "ReportedComment_ID", "dbo.Comments");
            DropForeignKey("dbo.Reports", "ReportedBlogPost_ID", "dbo.BlogPosts");
            DropForeignKey("dbo.Blocks", "BlockingUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Blocks", "BlockedUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Reports", new[] { "ReportingUser_Id" });
            DropIndex("dbo.Reports", new[] { "ReportedComment_ID" });
            DropIndex("dbo.Reports", new[] { "ReportedBlogPost_ID" });
            DropIndex("dbo.Blocks", new[] { "BlockingUser_Id" });
            DropIndex("dbo.Blocks", new[] { "BlockedUser_Id" });
            DropTable("dbo.Reports");
            DropTable("dbo.Blocks");
        }
    }
}
