namespace RatedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamePlayerModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.FilePathPlayers", name: "Players_Id", newName: "Player_Id");
            RenameIndex(table: "dbo.FilePathPlayers", name: "IX_Players_Id", newName: "IX_Player_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.FilePathPlayers", name: "IX_Player_Id", newName: "IX_Players_Id");
            RenameColumn(table: "dbo.FilePathPlayers", name: "Player_Id", newName: "Players_Id");
        }
    }
}
