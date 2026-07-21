using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AdminUser : User
    {
        public AdminUser(
      int id,
      string username,
      string email,
      string password)
        {
            Id = id;
            Username = username;
            Email = email;
            Password = password;
            Role = Role.Admin;
            IsVerified = true;
        }
 
    }
}
