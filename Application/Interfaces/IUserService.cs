using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(
            string username,
            string email,
            string password);

        User LoginUser(
            string email,
            string password);

        void SendVerificationCode(
            string email,
            string verificationCode);

        bool VerifyUser(
            string email,
            string verificationCode);

        void LogoutUser(
            string email);

        decimal GetBalance(
            string email);

        void Deposit(
            string email,
            decimal amount);

        void Withdraw(
            string email,
            decimal amount);
    }
}
