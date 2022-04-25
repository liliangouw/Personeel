namespace Personeel.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetrainInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrainInfoes", "UserId", "dbo.Users");
            DropIndex("dbo.TrainInfoes", new[] { "UserId" });
            AddColumn("dbo.TrainInfoes", "Sort", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.TrainInfoes", "FilePath", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropColumn("dbo.TrainInfoes", "UserId");
            DropColumn("dbo.TrainInfoes", "Roletext");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrainInfoes", "Roletext", c => c.String(nullable: false, storeType: "ntext"));
            AddColumn("dbo.TrainInfoes", "UserId", c => c.Guid(nullable: false));
            DropColumn("dbo.TrainInfoes", "FilePath");
            DropColumn("dbo.TrainInfoes", "Sort");
            CreateIndex("dbo.TrainInfoes", "UserId");
            AddForeignKey("dbo.TrainInfoes", "UserId", "dbo.Users", "Id");
        }
    }
}
