using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserDataManager _userDataManager;
        public UserService(IUserDataManager userDataManager)
        {
            _userDataManager = userDataManager;
        }
        public void RegisterUser(string username, string password, string v)
        {
            var existingUser = _userDataManager.GetUserByUsername(username);
            if (existingUser != null)
            {
                throw new Exception("user with this username already exists");
            }

            var verificationCode = new Random().Next(1000, 9999).ToString();
            var users = _userDataManager.GetAllUsers();
            int newId = users.Count == 0 ? 1 : users.Max(x => x.Id) + 1;
            var newUser = new User
                {
                    Id = newId,
                    Username = username,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    IsVerified = false,
                    VerificationCode = verificationCode
                };
                _userDataManager.CreateUser(newUser);
             //   _userDataManager.UpdateUser(_userDataManager.GetAllUsers);
        }
        }
    }
