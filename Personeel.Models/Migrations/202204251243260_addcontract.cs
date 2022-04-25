namespace Personeel.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcontract : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        Years = c.Int(nullable: false),
                        DeadLine = c.DateTime(nullable: false),
                        FilePath = c.String(nullable: false, maxLength: 100, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserGuid)
                .Index(t => t.UserGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "UserGuid", "dbo.Users");
            DropIndex("dbo.Contracts", new[] { "UserGuid" });
            DropTable("dbo.Contracts");
        }
    }
}
