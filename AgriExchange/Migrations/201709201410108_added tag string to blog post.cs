namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtagstringtoblogpost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "Tags", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPosts", "Tags");
        }
    }
}
