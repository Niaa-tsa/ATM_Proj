using Application.Services;
using Domain.Interfaces;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces.Application.Interfaces;

namespace UI.Menus
{
    // ადმინისტრატორის მართვის მენიუ.
    public class AdminMenu : BaseMenu
    {
        public override void Show()
        {

            while (true)
            {
                Console.WriteLine("\n--- ADMIN MENU ---");
                Console.WriteLine("1. View Loans");
                Console.WriteLine("2. Approve Loan");
                Console.WriteLine("3. Reject Loan");
                Console.WriteLine("4. View Users");
                Console.WriteLine("5. Delete User");
                Console.WriteLine("6. Logout");


                string choice =
                    Console.ReadLine();


                switch (choice)
                {

                    case "1":
                        ViewLoans();
                        break;


                    case "2":
                        ApproveLoan();
                        break;


                    case "3":
                        RejectLoan();
                        break;

                    case "4":
                        ViewUsers();
                        break;


                    case "5":
                        DeleteUser();
                        break;


                    case "6":
                        return;

                    default:
                        Console.WriteLine("Wrong option");
                        break;

                }

            }

        }
        private readonly ILoanService _loanService;
        private readonly IUserDataManager _userRepository;


        public AdminMenu(
     ILoanService loanService,
     IUserDataManager userRepository)
        {
            _loanService = loanService;
            _userRepository = userRepository;
        }
        private void ViewLoans()
        {
            var loans = _loanService.GetLoans();


            foreach (var loan in loans)
            {
                Console.WriteLine(
                    $"ID:{loan.Id} User:{loan.UserId} Amount:{loan.Amount} Status:{loan.Status}"
                );
            }
        }



        private void ApproveLoan()
        {
            try
            {
                Console.Write("Loan ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID");
                    return;
                }

                _loanService.ApproveLoan(id);

                Console.WriteLine("Loan approved");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        private void RejectLoan()
        {
            try
            {
                Console.Write("Loan ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid ID");
                    return;
                }

                _loanService.RejectLoan(id);

                Console.WriteLine("Loan rejected");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ViewUsers()
        {
            var users = _userRepository.GetAllUsers();


            foreach (var user in users)
            {
                Console.WriteLine(
                    $"ID: {user.Id} | Username: {user.Username} | Email: {user.Email} | Role: {user.Role}"
                );
            }
        }



        private void DeleteUser()
        {

            try
            {
                Console.Write("User ID: ");

                int id = int.Parse(Console.ReadLine());


                _userRepository.DeleteUser(id);


                Console.WriteLine("User deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

        

