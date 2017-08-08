namespace RatedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlayersFilePaths", newName: "FilePathPlayers");
            DropPrimaryKey("dbo.FilePathPlayers");
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        TournamentId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        Tournament_Id = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.TournamentId, t.PlayerId })
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.PlayerId)
                .Index(t => t.Tournament_Id);
            
            AddPrimaryKey("dbo.FilePathPlayers", new[] { "FilePath_Id", "Players_Id" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.Attendances", "PlayerId", "dbo.Player");
            DropIndex("dbo.Attendances", new[] { "Tournament_Id" });
            DropIndex("dbo.Attendances", new[] { "PlayerId" });
            DropPrimaryKey("dbo.FilePathPlayers");
            DropTable("dbo.Attendances");
            AddPrimaryKey("dbo.FilePathPlayers", new[] { "Players_Id", "FilePath_Id" });
            RenameTable(name: "dbo.FilePathPlayers", newName: "PlayersFilePaths");
        }
    }
}
