using Application.Services;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces.Application.Interfaces;

namespace UI.Menus
{
    // კლიენტის ფუნქციების მენიუ.
    public class ClientMenu : BaseMenu
    {
        private readonly IUserService _userService;
        private readonly ILoanService _loanService;
        private readonly User _currentUser;


        public ClientMenu(
     IUserService userService,
     ILoanService loanService,
     User currentUser)
        {
            _userService = userService;
            _loanService = loanService;
            _currentUser = currentUser;
        }

        public override void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- CLIENT MENU ---");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Request Loan");
                Console.WriteLine("5. Change Password");
                Console.WriteLine("6. Logout");
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
                            RequestLoan();
                            break;

                        case "5":
                            ChangePassword();
                            break;


                        case "6":
                            _userService.LogoutUser(_currentUser.Email);
                            Console.WriteLine("Logged out");
                            return;


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
            decimal balance =
                _userService.GetBalance(_currentUser.Email);

            Console.WriteLine(
                $"Balance: {balance}"
            );
        }


        private void Deposit()
        {
            Console.Write("Amount: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount");
                return;
            }

            _userService.Deposit(
                _currentUser.Email,
                amount
            );

            Console.WriteLine("Money added");
        }


        private void Withdraw()
        {
            Console.Write("Amount: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount");
                return;
            }

            _userService.Withdraw(
                _currentUser.Email,
                amount
            );

            Console.WriteLine("Money withdrawn");
        }
        private void RequestLoan()
        {
            Console.Write("Loan amount: ");

            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid amount");
                return;
            }


            _loanService.RequestLoan(
                _currentUser.Id,
                amount
            );


            Console.WriteLine(
            "Loan request sent");
        }
        private void ChangePassword()
        {
            Console.Write("Old password: ");
            string oldPassword = Console.ReadLine();


            Console.Write("New password: ");
            string newPassword = Console.ReadLine();


            _userService.ChangePassword(
                _currentUser.Email,
                oldPassword,
                newPassword
            );


            Console.WriteLine(
                "Password changed successfully"
            );
        }
    }
    }

