using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using UI.Menus;

namespace UI
{
    // ქმნის და აკავშირებს პროექტის ყველა საჭირო სერვისს.
    public static class ServiceContainer
    {
        public static MainMenu CreateMainMenu()
        {
            IUserDataManager repository = new UserRepository();


            DataInitializer.CreateAdmin(
                (UserRepository)repository
            );


            EmailService emailService =
                new EmailService();


            ILoggerService logger =
                new LoggerService();


            UserService userService =
                new UserService(
                    repository,
                    emailService,
                    logger
                );


            ILoanRepository loanRepository =
                new LoanRepository();


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


            return menu;
        }
    }
}

