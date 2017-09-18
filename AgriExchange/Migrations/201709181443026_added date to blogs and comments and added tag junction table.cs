namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddatetoblogsandcommentsandaddedtagjunctiontable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Blog_ID", "dbo.BlogPosts");
            DropIndex("dbo.Tags", new[] { "Blog_ID" });
            CreateTable(
                "dbo.BlogTags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Blog_ID = c.Int(),
                        Tag_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogPosts", t => t.Blog_ID)
                .ForeignKey("dbo.Tags", t => t.Tag_ID)
                .Index(t => t.Blog_ID)
                .Index(t => t.Tag_ID);
            
            AddColumn("dbo.BlogPosts", "DatePosted", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "PostDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Tags", "Blog_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Blog_ID", c => c.Int());
            DropForeignKey("dbo.BlogTags", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.BlogTags", "Blog_ID", "dbo.BlogPosts");
            DropIndex("dbo.BlogTags", new[] { "Tag_ID" });
            DropIndex("dbo.BlogTags", new[] { "Blog_ID" });
            DropColumn("dbo.Comments", "PostDate");
            DropColumn("dbo.BlogPosts", "DatePosted");
            DropTable("dbo.BlogTags");
            CreateIndex("dbo.Tags", "Blog_ID");
            AddForeignKey("dbo.Tags", "Blog_ID", "dbo.BlogPosts", "ID");
        }
    }
}
