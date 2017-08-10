namespace RatedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilePathRefactor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FilePathPlayers", "FilePath_Id", "dbo.FilePaths");
            DropForeignKey("dbo.FilePathPlayers", "Player_Id", "dbo.Players");
            DropIndex("dbo.FilePathPlayers", new[] { "FilePath_Id" });
            DropIndex("dbo.FilePathPlayers", new[] { "Player_Id" });
            AddColumn("dbo.Players", "FilePathId", c => c.Int(nullable: false));
            DropTable("dbo.FilePathPlayers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FilePathPlayers",
                c => new
                    {
                        FilePath_Id = c.Int(nullable: false),
                        Player_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilePath_Id, t.Player_Id });
            
            DropColumn("dbo.Players", "FilePathId");
            CreateIndex("dbo.FilePathPlayers", "Player_Id");
            CreateIndex("dbo.FilePathPlayers", "FilePath_Id");
            AddForeignKey("dbo.FilePathPlayers", "Player_Id", "dbo.Players", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FilePathPlayers", "FilePath_Id", "dbo.FilePaths", "Id", cascadeDelete: true);
        }
    }
}
