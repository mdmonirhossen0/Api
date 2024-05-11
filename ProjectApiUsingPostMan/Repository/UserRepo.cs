using ProjectApiUsingPostMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectApiUsingPostMan.Repository
{
    public class UserRepo: IDisposable
    {
        AppDbContext _db = new AppDbContext();

        public User ValidateUser(string userName, string password)
        {
            return _db.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && u.Password == password);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}