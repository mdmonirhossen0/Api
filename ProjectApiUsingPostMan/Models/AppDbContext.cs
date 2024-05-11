using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectApiUsingPostMan.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() : base("AppDbContext") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TaskDetail> TaskDetails { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}