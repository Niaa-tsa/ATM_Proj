using Application.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Helpers;
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
        private readonly ILoggerService _logger;
        public UserService(
     IUserDataManager userDataManager,
     EmailService emailService,
     ILoggerService logger)
        {
            _userDataManager = userDataManager;
            _emailService = emailService;
            _logger = logger;
        }
        public void RegisterUser(string username, string email, string password)
        {
            if (!ValidationHelper.IsNotEmpty(username))
                throw new Exception("Username is required");

            if (!ValidationHelper.IsValidEmail(email))
                throw new Exception("Invalid email");

            if (!ValidationHelper.IsValidPassword(password))
                throw new Exception("Password must be at least 4 characters");

            var existingUser = _userDataManager.GetUserByEmail(email);
            if (existingUser != null)
                throw new Exception("User already exists");


            var verificationCode = new Random().Next(1000, 9999).ToString();

            var users = _userDataManager.GetAllUsers();
            int newId = users.Count == 0 ? 1 : users.Max(x => x.Id) + 1;


            var newUser = new ClientUser(
      newId,
      username,
      email,
      BCrypt.Net.BCrypt.HashPassword(password),
      0
  );

            newUser.IsVerified = false;
            newUser.VerificationCode = verificationCode;
            _userDataManager.CreateUser(newUser);
            _logger.Log($"New user registered: {email}");
            SendVerificationCode(email, verificationCode);
        }

        public void SendVerificationCode(string email, string verificationCode)
        {
            _emailService.SeeEmail(email, "verification code", verificationCode);
        }
        public bool VerifyUser(string email, string verificationCode)
        {
            User user = _userDataManager.GetUserByEmail(email);
            if (user == null)
            {
       
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

                    _logger.Log($"User logged in: {email}");

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
            _logger.Log($"User {email} logged out");

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
            if (!ValidationHelper.IsPositive(amount))
                throw new Exception("Amount must be positive");

            var user = _userDataManager.GetUserByEmail(email);

            if (user == null)
                throw new Exception("User not found");


            if (user is ClientUser client)
            {
                client.Deposit(amount);

                _userDataManager.UpdateUser(client);
            }
            else
            {
                throw new Exception("Only clients can deposit");
            }


            _logger.Log($"{email} deposited {amount}");
        }
        public void Withdraw(string email, decimal amount)
        {
            if (!ValidationHelper.IsPositive(amount))
                throw new InvalidAmountException("Amount must be positive");


            var user = _userDataManager.GetUserByEmail(email);

            if (user == null)
                throw new Exception("User not found");


            if (user is ClientUser client)
            {
                client.Withdraw(amount);

                _userDataManager.UpdateUser(client);
            }
            else
            {
                throw new Exception("Only clients can withdraw");
            }


            _logger.Log($"{email} withdrew {amount}");
        }


    }
}
