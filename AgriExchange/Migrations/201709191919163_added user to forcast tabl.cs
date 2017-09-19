namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedusertoforcasttabl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Forcasts", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Forcasts", "User_Id");
            AddForeignKey("dbo.Forcasts", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forcasts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Forcasts", new[] { "User_Id" });
            DropColumn("dbo.Forcasts", "User_Id");
        }
    }
}
