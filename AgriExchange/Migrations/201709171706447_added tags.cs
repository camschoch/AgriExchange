namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                        Blog_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogPosts", t => t.Blog_ID)
                .Index(t => t.Blog_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Blog_ID", "dbo.BlogPosts");
            DropIndex("dbo.Tags", new[] { "Blog_ID" });
            DropTable("dbo.Tags");
        }
    }
}
