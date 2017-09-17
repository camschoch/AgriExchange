namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbloglikescommentsandcommentlikes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogLikes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Blog_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogPosts", t => t.Blog_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Blog_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CommentLikes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comment_ID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Comments", t => t.Comment_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Comment_ID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Blog_ID = c.Int(),
                        Uset_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogPosts", t => t.Blog_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Uset_Id)
                .Index(t => t.Blog_ID)
                .Index(t => t.Uset_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentLikes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentLikes", "Comment_ID", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Uset_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Blog_ID", "dbo.BlogPosts");
            DropForeignKey("dbo.BlogLikes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogLikes", "Blog_ID", "dbo.BlogPosts");
            DropIndex("dbo.Comments", new[] { "Uset_Id" });
            DropIndex("dbo.Comments", new[] { "Blog_ID" });
            DropIndex("dbo.CommentLikes", new[] { "User_Id" });
            DropIndex("dbo.CommentLikes", new[] { "Comment_ID" });
            DropIndex("dbo.BlogLikes", new[] { "User_Id" });
            DropIndex("dbo.BlogLikes", new[] { "Blog_ID" });
            DropTable("dbo.Comments");
            DropTable("dbo.CommentLikes");
            DropTable("dbo.BlogLikes");
        }
    }
}
