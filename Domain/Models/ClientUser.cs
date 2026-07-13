using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class ClientUser : User
    {
        public decimal Balance { get; protected set; }

        public ClientUser(int id, string username, string password, decimal balance, Role role)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = Role.Client;
            Balance = balance;
        }

        public void Deposit(decimal amount) { }

        public void Withdraw(decimal amount) { }
    }
}
