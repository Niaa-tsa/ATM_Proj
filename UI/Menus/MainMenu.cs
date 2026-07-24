using Application.Interfaces;
using Application.Interfaces;
using Application.Interfaces.Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Menus
{
    // პროგრამის მთავარი მენიუ.
    public class MainMenu 
    {

        private readonly IUserService _userService;
        private readonly ILoanService _loanService;
        private readonly IUserDataManager _userRepository;

        public MainMenu(
     IUserService userService,
     ILoanService loanService,
     IUserDataManager userRepository)
        {
            _userService = userService;
            _loanService = loanService;
            _userRepository = userRepository;
        }


        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- ATM SYSTEM ---");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Verify Account");
                Console.WriteLine("0. Exit");


                string choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        Register();
                        break;


                    case "2":
                        Login();
                        break;

                    case "3":
                        Verify();
                        break;

                    case "0":
                        return;


                    default:
                        Console.WriteLine("Wrong option");
                        break;
                }
            }
        }



        private void Register()
        {
            try
            {
                Console.Write("Username: ");
                string username = Console.ReadLine();


                Console.Write("Email: ");
                string email = Console.ReadLine();


                Console.Write("Password: ");
                string password = Console.ReadLine();


                _userService.RegisterUser(
                    username,
                    email,
                    password
                );

                Console.WriteLine("Verification code was sent to email.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Verify()
        {
            try
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();

                Console.Write("Code: ");
                string code = Console.ReadLine();

                bool isVerified = _userService.VerifyUser(email, code);

                if (isVerified)
                    Console.WriteLine("Account verified!");
                else
                    Console.WriteLine("Wrong code!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void Login()
        {
            try
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();


                Console.Write("Password: ");
                string password = Console.ReadLine();


                var user = _userService.LoginUser(
                    email,
                    password
                );


                Console.WriteLine(
                    $"Welcome {user.Username}"
                );
                BaseMenu menu;


                if (user.Role == Role.Admin)
                {
                    menu = new AdminMenu(
                        _loanService,
                        _userRepository
                    );
                }
                else
                {
                    if (!user.IsVerified)
                    {
                        Console.WriteLine("Account is not verified!");
                        return;
                    }


                    menu = new ClientMenu(
                        _userService,
                        _loanService,
                        user
                    );
                }
            


                menu.Show();
            }
            catch (AppException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }
    }
}

