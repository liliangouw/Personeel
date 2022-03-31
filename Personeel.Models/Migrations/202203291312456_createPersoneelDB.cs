namespace Personeel.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createPersoneelDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Announces",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        AnnounceTitle = c.String(nullable: false, maxLength: 50, unicode: false),
                        AnnounceText = c.String(nullable: false, storeType: "ntext"),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 40, unicode: false),
                        Password = c.String(nullable: false, maxLength: 30, unicode: false),
                        ImagePath = c.String(nullable: false, maxLength: 300, unicode: false),
                        UserRightID = c.Guid(nullable: false),
                        UserNum = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10, unicode: false),
                        Gender = c.String(maxLength: 4, unicode: false),
                        Birthday = c.DateTime(nullable: false),
                        IDNumber = c.Int(nullable: false),
                        Wedlock = c.String(maxLength: 5, unicode: false),
                        Race = c.String(maxLength: 5, unicode: false),
                        Nativeplace = c.String(maxLength: 30, unicode: false),
                        Politic = c.String(maxLength: 5, unicode: false),
                        Phone = c.Int(nullable: false),
                        DepartmentID = c.Guid(nullable: false),
                        PositionID = c.Guid(nullable: false),
                        Basicmoney = c.Int(nullable: false),
                        Tiptopdegree = c.String(maxLength: 5, unicode: false),
                        School = c.String(maxLength: 30, unicode: false),
                        Beginworkdate = c.DateTime(nullable: false),
                        Beformdate = c.DateTime(nullable: false),
                        Notworkdate = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .ForeignKey("dbo.Positions", t => t.PositionID)
                .ForeignKey("dbo.UserRights", t => t.UserRightID)
                .Index(t => t.UserRightID)
                .Index(t => t.DepartmentID)
                .Index(t => t.PositionID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Depname = c.String(nullable: false, maxLength: 20, unicode: false),
                        Depdescribe = c.String(maxLength: 200, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Posname = c.String(nullable: false, maxLength: 20, unicode: false),
                        Posdescribe = c.String(maxLength: 200, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRights",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserPower = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AskForLeaves",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        LeaveSort = c.String(nullable: false, maxLength: 10, unicode: false),
                        LeaveReason = c.String(nullable: false, maxLength: 100, unicode: false),
                        LeaveIsPass = c.Boolean(nullable: false),
                        LeaveNotReason = c.String(maxLength: 100, unicode: false),
                        ApproveTime = c.DateTime(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ChangeSalaries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ChangeReason = c.String(nullable: false, maxLength: 100, unicode: false),
                        ChangedSalary = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.EncourageOrChastisements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Category = c.String(nullable: false, maxLength: 20, unicode: false),
                        Reason = c.String(nullable: false, maxLength: 100, unicode: false),
                        Money = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        EventSort = c.String(nullable: false, maxLength: 10, unicode: false),
                        EventReason = c.String(nullable: false, maxLength: 100, unicode: false),
                        IsPass = c.Boolean(nullable: false),
                        NotReason = c.String(nullable: false, maxLength: 100, unicode: false),
                        ApproveTime = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Operationlogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Logdes = c.String(nullable: false, maxLength: 50, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Regulatories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        Roletext = c.String(nullable: false, storeType: "ntext"),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SalaryInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        BasicSalary = c.Int(nullable: false),
                        EncourageOrChastisement = c.Int(nullable: false),
                        ShouldDays = c.Int(nullable: false),
                        ActualDays = c.Int(nullable: false),
                        Subsidies = c.Int(nullable: false),
                        Accumulationfund = c.Int(nullable: false),
                        SocialSecurity = c.Int(nullable: false),
                        Tax = c.Int(nullable: false),
                        ActualSalary = c.Int(nullable: false),
                        SalaryDate = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TrainInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        Roletext = c.String(nullable: false, storeType: "ntext"),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        TrainSort = c.String(nullable: false, maxLength: 20, unicode: false),
                        TrainDes = c.String(nullable: false, maxLength: 50, unicode: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        TrainResult = c.String(nullable: false, maxLength: 10, unicode: false),
                        TarinComment = c.String(nullable: false, maxLength: 200, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Transfers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        TransferReason = c.String(nullable: false, maxLength: 50, unicode: false),
                        TransferDep = c.String(nullable: false, maxLength: 20, unicode: false),
                        TransferPos = c.String(nullable: false, maxLength: 20, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transfers", "UserId", "dbo.Users");
            DropForeignKey("dbo.Trains", "UserId", "dbo.Users");
            DropForeignKey("dbo.TrainInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.SalaryInfoes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Regulatories", "UserId", "dbo.Users");
            DropForeignKey("dbo.Operationlogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.Events", "UserId", "dbo.Users");
            DropForeignKey("dbo.EncourageOrChastisements", "UserId", "dbo.Users");
            DropForeignKey("dbo.ChangeSalaries", "UserId", "dbo.Users");
            DropForeignKey("dbo.AskForLeaves", "UserId", "dbo.Users");
            DropForeignKey("dbo.Announces", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserRightID", "dbo.UserRights");
            DropForeignKey("dbo.Users", "PositionID", "dbo.Positions");
            DropForeignKey("dbo.Users", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Transfers", new[] { "UserId" });
            DropIndex("dbo.Trains", new[] { "UserId" });
            DropIndex("dbo.TrainInfoes", new[] { "UserId" });
            DropIndex("dbo.SalaryInfoes", new[] { "UserId" });
            DropIndex("dbo.Regulatories", new[] { "UserId" });
            DropIndex("dbo.Operationlogs", new[] { "UserId" });
            DropIndex("dbo.Events", new[] { "UserId" });
            DropIndex("dbo.EncourageOrChastisements", new[] { "UserId" });
            DropIndex("dbo.ChangeSalaries", new[] { "UserId" });
            DropIndex("dbo.AskForLeaves", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "PositionID" });
            DropIndex("dbo.Users", new[] { "DepartmentID" });
            DropIndex("dbo.Users", new[] { "UserRightID" });
            DropIndex("dbo.Announces", new[] { "UserId" });
            DropTable("dbo.Transfers");
            DropTable("dbo.Trains");
            DropTable("dbo.TrainInfoes");
            DropTable("dbo.SalaryInfoes");
            DropTable("dbo.Regulatories");
            DropTable("dbo.Operationlogs");
            DropTable("dbo.Events");
            DropTable("dbo.EncourageOrChastisements");
            DropTable("dbo.ChangeSalaries");
            DropTable("dbo.AskForLeaves");
            DropTable("dbo.UserRights");
            DropTable("dbo.Positions");
            DropTable("dbo.Departments");
            DropTable("dbo.Users");
            DropTable("dbo.Announces");
        }
    }
}
