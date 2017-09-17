namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedblogposttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPosts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BlogPosts", new[] { "User_Id" });
            DropTable("dbo.BlogPosts");
        }
    }
}
