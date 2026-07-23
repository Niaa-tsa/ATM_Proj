using Application.Interfaces;
using Application.Interfaces.Application.Interfaces;
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


            IEmailService emailService =
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

            ILoanService loanService =
                new LoanService(
                    loanRepository,
                    repository,
                    logger
                );

            MainMenu menu =
                new MainMenu(
                    userService,
                    loanService,
                    repository
                );


            return menu;
        }
    }
}

