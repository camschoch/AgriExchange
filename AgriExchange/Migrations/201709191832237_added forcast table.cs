namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedforcasttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forcasts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        HighTemp = c.Double(nullable: false),
                        LowTemp = c.Double(nullable: false),
                        Percipitation = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Forcasts");
        }
    }
}
