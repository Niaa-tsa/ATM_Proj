using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class AdminUser : User
    {
        public AdminUser(int id, string username, string password, Role role)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = Role.Admin;
        }
    }
}
