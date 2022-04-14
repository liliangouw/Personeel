namespace Personeel.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "UserGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.AskForLeaves", "LeaveState", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddColumn("dbo.Events", "IsPassState", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.Events", "NotReason", c => c.String(maxLength: 100, unicode: false));
            DropColumn("dbo.AskForLeaves", "LeaveIsPass");
            DropColumn("dbo.Events", "IsPass");
            DropColumn("dbo.Events", "ApproveTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "ApproveTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "IsPass", c => c.Boolean(nullable: false));
            AddColumn("dbo.AskForLeaves", "LeaveIsPass", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Events", "NotReason", c => c.String(nullable: false, maxLength: 100, unicode: false));
            DropColumn("dbo.Events", "IsPassState");
            DropColumn("dbo.AskForLeaves", "LeaveState");
            DropColumn("dbo.Departments", "UserGuid");
        }
    }
}
