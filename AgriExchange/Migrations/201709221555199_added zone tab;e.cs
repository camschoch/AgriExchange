namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedzonetabe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZoneName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Zones");
        }
    }
}
