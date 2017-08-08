namespace RatedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tournaments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FilePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Rating = c.Int(nullable: false),
                        Games = c.Int(nullable: false),
                        Tournament_Id = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tournaments", t => t.Tournament_Id)
                .Index(t => t.Tournament_Id);
            
            CreateTable(
                "dbo.Tournaments",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                        AdminId = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayersFilePaths",
                c => new
                    {
                        Players_Id = c.Int(nullable: false),
                        FilePath_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Players_Id, t.FilePath_Id })
                .ForeignKey("dbo.Player", t => t.Players_Id, cascadeDelete: true)
                .ForeignKey("dbo.FilePaths", t => t.FilePath_Id, cascadeDelete: true)
                .Index(t => t.Players_Id)
                .Index(t => t.FilePath_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Player", "Tournament_Id", "dbo.Tournaments");
            DropForeignKey("dbo.PlayersFilePaths", "FilePath_Id", "dbo.FilePaths");
            DropForeignKey("dbo.PlayersFilePaths", "Players_Id", "dbo.Player");
            DropIndex("dbo.PlayersFilePaths", new[] { "FilePath_Id" });
            DropIndex("dbo.PlayersFilePaths", new[] { "Players_Id" });
            DropIndex("dbo.Player", new[] { "Tournament_Id" });
            DropTable("dbo.PlayersFilePaths");
            DropTable("dbo.Tournaments");
            DropTable("dbo.Player");
            DropTable("dbo.FilePaths");
        }
    }
}
