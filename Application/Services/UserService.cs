using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDataManager _userDataManager;
        private readonly EmailService _emailService;
        public UserService(IUserDataManager userDataManager, EmailService emailService)
        {
            _userDataManager = userDataManager;
            _emailService = emailService;
            _emailService = new EmailService();
        }
        public void RegisterUser(string username, string email, string password)
        {
            var existingUser = _userDataManager.GetUserByEmail(email);
            if (existingUser != null)
            {
                throw new Exception("user with this email already exists");
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
                VerificationCode = verificationCode,
                Email = email,
            };
            _userDataManager.CreateUser(newUser);
            SendVerificationCode(email, verificationCode);
            //   _userDataManager.UpdateUser(_userDataManager.GetAllUsers);
        }

      

        // public void RegisterUser(string username, string password, string v)
        // {
        //   throw new NotImplementedException();
        // }

        public void SendVerificationCode(string email, string verificationCode)
        {
            _emailService.SeeEmail(email, "verification code", verificationCode);
        }
        public bool VerifyUser(string email, string verificationCode)
        {
            User user = _userDataManager.GetUserByEmail(email);
            if (user == null)
            {
                Console.WriteLine("user not found");
                throw new ArgumentException("user not found ");
            }
            if (user.VerificationCode == verificationCode)
            {
                user.IsVerified = true;
                _userDataManager.UpdateUser(user);
                return true;
            }
            return false;
        }
        public User LoginUser(string email, string password)
        {
            User us = _userDataManager.GetUserByEmail(email);
            if (us != null && us.IsVerified)
            {
                if (BCrypt.Net.BCrypt.Verify(password, us.Password))
                {
                    us.LastLogin = DateTime.Now;
                    _userDataManager.UpdateUser(us);
                    return us;
                }
            }
            throw new Exception("Invalid email or not verified");
        }

        public void LogoutUser(string email)
        {
            User us = _userDataManager.GetUserByEmail(email);
            us.LastLogin = null;
            _userDataManager.UpdateUser(us);

        }
        public decimal GetBalance(string email)
        {
            var user = _userDataManager.GetUserByEmail(email);

            if (user is ClientUser client)
            {
                return client.Balance;
            }

            throw new Exception("User is not a client");
        }
        public void Deposit(string email, decimal amount)
        {
            var user = _userDataManager.GetUserByEmail(email);

            if (user is ClientUser client)
            {
                client.Deposit(amount);
                _userDataManager.UpdateUser(client);
                return;
            }

            throw new Exception("User is not a client");
        }
        public void Withdraw(string email, decimal amount)
        {
            var user = _userDataManager.GetUserByEmail(email);

            if (user is ClientUser client)
            {
                client.Withdraw(amount);
                _userDataManager.UpdateUser(client);
                return;
            }

            throw new Exception("User is not a client");
        }
    }
}
