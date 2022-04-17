namespace Personeel.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangSalaryDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Checkings", "DayTime", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.SalaryInfoes", "SalaryDate", c => c.String(maxLength: 30, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalaryInfoes", "SalaryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Checkings", "DayTime", c => c.String());
        }
    }
}
