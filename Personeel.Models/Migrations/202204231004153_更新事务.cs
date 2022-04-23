namespace Personeel.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 更新事务 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AskForLeaves", "LeaveState", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "IsPassState", c => c.Int(nullable: false));
            AlterColumn("dbo.PayRollAccounts", "BasicSalary", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PayRollAccounts", "BasicSalary", c => c.String());
            AlterColumn("dbo.Events", "IsPassState", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.AskForLeaves", "LeaveState", c => c.String(nullable: false, maxLength: 10, unicode: false));
        }
    }
}
