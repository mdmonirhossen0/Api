namespace ProjectApiUsingPostMan.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeCode = c.String(),
                        EmployeeName = c.String(),
                        JoiningDate = c.DateTime(nullable: false),
                        IsRegular = c.Boolean(nullable: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.TaskDetails",
                c => new
                    {
                        TaskDetailId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskDetailId)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TaskId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Roles = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskDetails", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.TaskDetails", "TaskId", "dbo.Tasks");
            DropIndex("dbo.TaskDetails", new[] { "TaskId" });
            DropIndex("dbo.TaskDetails", new[] { "EmployeeId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tasks");
            DropTable("dbo.TaskDetails");
            DropTable("dbo.Employees");
        }
    }
}
