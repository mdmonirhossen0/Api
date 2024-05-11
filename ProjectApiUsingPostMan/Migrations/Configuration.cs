namespace ProjectApiUsingPostMan.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectApiUsingPostMan.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectApiUsingPostMan.Models.AppDbContext context)
        {
            context.Tasks.AddOrUpdate(x => x.TaskId,
                new Models.Task() { TaskId = 1, TaskName = "Product Development", AmountPaid = 25000 },
                new Models.Task() { TaskId = 2, TaskName = "Inventory Management", AmountPaid = 18000 },
                new Models.Task() { TaskId = 1, TaskName = "Supply Chain Optimization", AmountPaid = 35000 });

            context.Users.AddOrUpdate(x => x.UserId,
                new Models.User() { UserId = 1, UserName = "Admin", Password = "1234", Email = "admin@gmail.com", Roles = "Admin" },
                new Models.User() { UserId = 2, UserName = "User", Password = "1234", Email = "User@gmail.com", Roles = "User" });
        }
    }
}
