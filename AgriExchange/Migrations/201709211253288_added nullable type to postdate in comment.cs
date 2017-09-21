namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednullabletypetopostdateincomment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "PostDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "PostDate", c => c.DateTime(nullable: false));
        }
    }
}
