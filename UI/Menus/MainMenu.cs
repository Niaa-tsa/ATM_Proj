using Application.Services;
using Domain.Enums;
using Domain.Exceptions;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Menus
{
    // პროგრამის მთავარი მენიუ.
    public class MainMenu
    {

        private readonly UserService _userService;
        private readonly LoanService _loanService;

        public MainMenu(
      UserService userService,
      LoanService loanService)
        {
            _userService = userService;
            _loanService = loanService;
        }


        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n--- ATM SYSTEM ---");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
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

                Console.WriteLine("Enter verification code:");
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

                if (user.Role == Role.Admin)
                {
                    AdminMenu menu =
         new AdminMenu(
             _loanService,
             new UserRepository()
         );

                    menu.Show();
                }
                else
                {
                    ClientMenu menu = new ClientMenu(
         _userService,
         _loanService,
         user
     );

                    menu.Show();
                }
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

