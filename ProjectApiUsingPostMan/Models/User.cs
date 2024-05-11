using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectApiUsingPostMan.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
    }
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsRegular { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<TaskDetail> TaskDetails { get; set; }
    }
    public class TaskDetail
    {
        public int TaskDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public decimal AmountPaid { get; set; }
    }
    public class EmployeeRequest
    {
        public Employee Employee { get; set; }
        public byte[] ImageFile { get; set; }
        public string ImageFileName { get; set; }
    }
}