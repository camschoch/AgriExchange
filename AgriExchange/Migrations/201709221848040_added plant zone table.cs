namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedplantzonetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlantZones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Plant_ID = c.Int(),
                        Zone_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Plants", t => t.Plant_ID)
                .ForeignKey("dbo.Zones", t => t.Zone_ID)
                .Index(t => t.Plant_ID)
                .Index(t => t.Zone_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlantZones", "Zone_ID", "dbo.Zones");
            DropForeignKey("dbo.PlantZones", "Plant_ID", "dbo.Plants");
            DropIndex("dbo.PlantZones", new[] { "Zone_ID" });
            DropIndex("dbo.PlantZones", new[] { "Plant_ID" });
            DropTable("dbo.PlantZones");
        }
    }
}
