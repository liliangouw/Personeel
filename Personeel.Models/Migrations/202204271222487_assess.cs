namespace Personeel.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class assess : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assesses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AssessName = c.String(nullable: false, maxLength: 20, unicode: false),
                        AssessType = c.String(nullable: false, maxLength: 10, unicode: false),
                        UserGuid = c.Guid(nullable: false),
                        Result = c.String(nullable: false, maxLength: 10, unicode: false),
                        State = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserGuid)
                .Index(t => t.UserGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assesses", "UserGuid", "dbo.Users");
            DropIndex("dbo.Assesses", new[] { "UserGuid" });
            DropTable("dbo.Assesses");
        }
    }
}
