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


            User admin = new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@gmail.com",
                Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                Role = Role.Admin,
                IsVerified = true
            };


            repository.CreateUser(admin);
        }
    }
}
