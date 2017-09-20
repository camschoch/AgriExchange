namespace AgriExchange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedspellingerrorUsettoUserinbloglikestable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "Uset_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_Uset_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_User_Id", newName: "IX_Uset_Id");
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "Uset_Id");
        }
    }
}
