using Application.Services;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using UI.Menus;

namespace UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                IUserDataManager repository =
                    new UserRepository();


                EmailService emailService =
                    new EmailService();


                UserService userService =
                    new UserService(
                        repository,
                        emailService
                    );

                LoanRepository loanRepository = new LoanRepository();

                LoanService loanService =
                    new LoanService(
                        loanRepository,
                        repository
                    );


                MainMenu menu =
                    new MainMenu(
                        userService,
                        loanService
                    );


                menu.Show();
            }
        }
    }
}
