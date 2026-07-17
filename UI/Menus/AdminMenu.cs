using Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Menus
{
    public class AdminMenu
    {
            public void Show()
            {

                while (true)
                {

                    Console.WriteLine("\nADMIN MENU");

                    Console.WriteLine("1. View Loans");
                    Console.WriteLine("2. Approve Loan");
                    Console.WriteLine("3. Reject Loan");
                    Console.WriteLine("4. Logout");


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
                            return;

                    default:
                        Console.WriteLine("Wrong option");
                        break;

                }

                }

        }
        private readonly LoanService _loanService;


        public AdminMenu(LoanService loanService)
        {
            _loanService = loanService;
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
            Console.Write("Loan ID: ");

            int id = int.Parse(Console.ReadLine());


            _loanService.ApproveLoan(id);


            Console.WriteLine("Loan approved");
        }



        private void RejectLoan()
        {
            Console.Write("Loan ID: ");

            int id = int.Parse(Console.ReadLine());


            _loanService.RejectLoan(id);


            Console.WriteLine("Loan rejected");
        }
    }
}

        

