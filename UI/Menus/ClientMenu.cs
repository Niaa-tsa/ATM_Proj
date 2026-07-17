using Application.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Menus
{
    public class ClientMenu
    {
        private readonly UserService _userService;
        private readonly User _currentUser;
        private readonly LoanService _loanService;
        public ClientMenu(
     UserService userService,
     LoanService loanService,
     User currentUser)
        {
            _userService = userService;
            _loanService = loanService;
            _currentUser = currentUser;
        }


        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- CLIENT MENU ---");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Logout");
                Console.WriteLine("5. Request Loan");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();


                try
                {
                    switch (choice)
                    {
                        case "1":
                            CheckBalance();
                            break;

                        case "2":
                            Deposit();
                            break;

                        case "3":
                            Withdraw();
                            break;

                        case "4":
                            _userService.LogoutUser(_currentUser.Email);
                            Console.WriteLine("Logged out");
                            return;

                        case "5":
                            RequestLoan();
                            break;

                        default:
                            Console.WriteLine("Wrong option");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        private void CheckBalance()
        {
            Console.WriteLine(
                $"Balance: {_currentUser.Balance}"
            );
        }


        private void Deposit()
        {
            Console.Write("Amount: ");

            decimal amount = decimal.Parse(Console.ReadLine());

            _userService.Deposit(
                _currentUser.Email,
                amount
            );

            Console.WriteLine("Money added");
        }


        private void Withdraw()
        {
            Console.Write("Amount: ");

            decimal amount = decimal.Parse(Console.ReadLine());

            _userService.Withdraw(
                _currentUser.Email,
                amount
            );

            Console.WriteLine("Money withdrawn");
        }
        private void RequestLoan()
        {
            Console.Write("Loan amount: ");

            decimal amount =
            decimal.Parse(Console.ReadLine());


            _loanService.RequestLoan(
                _currentUser.Id,
                amount
            );


            Console.WriteLine(
            "Loan request sent");
        }
    }
}
