namespace RatedIn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Attendances", new[] { "Tournament_Id" });
            AlterColumn("dbo.Attendances", "Tournament_Id", c => c.Byte());
            CreateIndex("dbo.Attendances", "Tournament_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Attendances", new[] { "Tournament_Id" });
            AlterColumn("dbo.Attendances", "Tournament_Id", c => c.Byte(nullable: false));
            CreateIndex("dbo.Attendances", "Tournament_Id");
        }
    }
}
