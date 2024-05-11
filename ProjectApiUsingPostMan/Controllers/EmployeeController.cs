using ProjectApiUsingPostMan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ProjectApiUsingPostMan.Controllers
{
    public class EmployeeController : ApiController
    {
        private AppDbContext db = new AppDbContext();
        [Authorize(Roles = ("User,Admin"))]
        public System.Object GetOrders()
        {
            var result = db.Employees.ToList();
            return result.OrderBy(o => o.EmployeeId);
        }
        [Authorize(Roles = ("User,Admin"))]
        public IHttpActionResult GetEmployeeById(int id)
        {
            var emp = (from a in db.Employees
                         where a.EmployeeId == id
                         select new
                         {
                             a.EmployeeId,
                             a.EmployeeCode,
                             a.EmployeeName,
                             a.IsRegular,
                             a.ImageUrl
                         }).FirstOrDefault();
            var taskDetails = (from a in db.TaskDetails
                                join b in db.Tasks on a.TaskId equals b.TaskId
                                where a.EmployeeId == id
                                select new
                                {
                                    a.EmployeeId,
                                    a.TaskId,
                                    b.TaskName,
                                    b.AmountPaid
                                }).ToList();
            return Ok(new { emp, taskDetails });
        }
        [Authorize(Roles = ("Admin"))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var emp = db.Employees.Find(id);
            var taskDetails = db.TaskDetails.Where(item => item.EmployeeId == id).ToList();
            foreach (var item in taskDetails)
            {
                db.TaskDetails.Remove(item);
            }
            db.Employees.Remove(emp);
            db.SaveChanges();
            return Ok("Employee and related items have been successfully deleted.");
        }
        [Authorize(Roles = ("Admin"))]
        public IHttpActionResult PostEmployee(EmployeeRequest request)
        {
            if (request.Employee == null)
            {
                return BadRequest("Employee data is Missing");
            }
            Employee obj = request.Employee;
            byte[] imageFile = request.ImageFile;
            if (imageFile != null && imageFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine("~/Images/", fileName);

                File.WriteAllBytes(HttpContext.Current.Server.MapPath(filePath), imageFile);
                obj.ImageUrl = filePath;
            }
            Employee emp = new Employee
            {
                EmployeeCode = obj.EmployeeCode,
                JoiningDate = obj.JoiningDate,
                EmployeeName = obj.EmployeeName,
                IsRegular = obj.IsRegular,
                ImageUrl = obj.ImageUrl,
            };
            db.Employees.Add(emp);
            db.SaveChanges();
            var empObj = db.Employees.FirstOrDefault(x => x.EmployeeCode == obj.EmployeeCode);
            if (empObj != null && obj.TaskDetails != null)
            {
                foreach (var item in obj.TaskDetails)
                {
                    TaskDetail taskdtls = new TaskDetail
                    {
                        EmployeeId = empObj.EmployeeId,
                        TaskId = item.TaskId,
                    };
                    db.TaskDetails.Add(taskdtls);
                }
            }
            db.SaveChanges();
            return Ok("Employee Saved Successfully");
        }
        [Authorize(Roles = ("Admin"))]
        public IHttpActionResult PutEmployee(int id, EmployeeRequest request)
        {
            Employee emp = db.Employees.FirstOrDefault(x => x.EmployeeId == id);
            if (id != request.Employee.EmployeeId)
            {
                return BadRequest();
            }
            if (emp == null)
            {
                return NotFound();
            }
            if (request.Employee == null)
            {
                return BadRequest("Employee data is missing.");
            }
            Employee obj = request.Employee;
            byte[] imageFile = request.ImageFile;
            if (imageFile != null && imageFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = Path.Combine("~/Images/", fileName);
                File.WriteAllBytes(HttpContext.Current.Server.MapPath(filePath), imageFile);
                obj.ImageUrl = filePath;
            }
            emp.EmployeeCode = obj.EmployeeCode;
            emp.JoiningDate = obj.JoiningDate;
            emp.EmployeeName = obj.EmployeeName;
            emp.IsRegular = obj.IsRegular;
            emp.ImageUrl = obj.ImageUrl;
            var existingtaskDetails = db.TaskDetails.Where(x => x.EmployeeId == emp.EmployeeId);
            db.TaskDetails.RemoveRange(existingtaskDetails);
            if (obj.TaskDetails != null)
            {
                foreach (var item in obj.TaskDetails)
                {
                    TaskDetail taskDtls = new TaskDetail
                    {
                        EmployeeId = emp.EmployeeId,
                        TaskId = item.TaskId
                    };
                    db.TaskDetails.Add(taskDtls);
                }
            }
            db.SaveChanges();
            return Ok("Employee and related items have been successfully updated.");
        }
    }
}
