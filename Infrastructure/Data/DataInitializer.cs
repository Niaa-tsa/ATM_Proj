using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BCrypt.Net;
namespace Infrastructure.Data
{
    public class DataInitializer
    {
        public static void CreateAdmin(UserRepository repository)
        {
            var users = repository.GetAllUsers();

            if (users.Any(x => x.Role == Role.Admin))
                return;


            AdminUser admin = new AdminUser(
     1,
     "admin",
     "admin@gmail.com",
     BCrypt.Net.BCrypt.HashPassword("admin123")
 );


            repository.CreateUser(admin);
        }
    }
}
