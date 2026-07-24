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
    // მომხმარებლის რეგისტრაცია და ავტორიზაცია
    public class UserService : IUserService
    {
        private readonly IUserDataManager _userDataManager;
        private readonly IEmailService _emailService;
        private readonly ILoggerService _logger;
        public UserService(
     IUserDataManager userDataManager,
    IEmailService emailService,
     ILoggerService logger)
        {
            _userDataManager = userDataManager;
            _emailService = emailService;
            _logger = logger;
        }
        // არეგისტრირებს ახალ მომხმარებელს და უგზავნის ვერიფიკაციის კოდს.
        public void RegisterUser(string username, string email, string password)
        {
            if (password.Length < 4)
                throw new Exception("Password must be at least 4 characters");

            if (_userDataManager.GetUserByEmail(email) != null)
                throw new UserAlreadyExistsException();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);


            var user = new ClientUser(
                _userDataManager.GetAllUsers().Count + 1,
                username,
                email,
                hashedPassword,
                0
            );

            user.IsVerified = false;


            string code = new Random()
                .Next(1000, 9999)
                .ToString();


            user.VerificationCode = code;


            _userDataManager.CreateUser(user);


            SendVerificationCode(email, code);
        }


        public void SendVerificationCode(
    string email,
    string verificationCode)
        {
            _emailService.SendVerificationEmail(
                email,
                verificationCode
            );
        }
        // ამოწმებს ვერიფიკაციის კოდს და ააქტიურებს ანგარიშს.
        public bool VerifyUser(string email, string code)
        {
            var user = _userDataManager.GetUserByEmail(email);

            if (user == null)
                throw new Exception("User not found");

            if (user.VerificationCode?.Trim() != code.Trim())
                return false;

            user.IsVerified = true; 

            _userDataManager.UpdateUser(user);

            return true;
        }
        // მომხმარებლის ავტორიზაცია.
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
            throw new InvalidCredentialsException();
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
        // ანგარიშზე თანხის შეტანა.
        public void Deposit(string email, decimal amount)
        {
            if (!ValidationHelper.IsPositive(amount))
                throw new Exception("Amount must be positive");

            var user = _userDataManager.GetUserByEmail(email);

            if (user == null)
                throw new UserNotFoundException();

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
        // ანგარიშიდან თანხის გამოტანა.
        public void Withdraw(string email, decimal amount)
        {
            if (!ValidationHelper.IsPositive(amount))
                throw new InvalidAmountException("Amount must be positive");


            var user = _userDataManager.GetUserByEmail(email);
            if (user == null)
                throw new UserNotFoundException();


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


        // პაროლის შეცვლა.
        public void ChangePassword(
    string email,
    string oldPassword,
    string newPassword)
        {
            var user = _userDataManager.GetUserByEmail(email);

            if (user == null)
                throw new UserNotFoundException();


            bool isCorrectPassword =
                BCrypt.Net.BCrypt.Verify(
                    oldPassword,
                    user.Password
                );


            if (!isCorrectPassword)
                throw new InvalidCredentialsException();


            if (newPassword.Length < 4)
                throw new Exception(
                    "Password must be at least 4 characters"
                );


            user.Password =
                BCrypt.Net.BCrypt.HashPassword(
                    newPassword
                );


            _userDataManager.UpdateUser(user);


            _logger.Log(
                $"{email} changed password"
            );
        }

    }
}
