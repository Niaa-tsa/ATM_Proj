using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure;
namespace Application.Services
{
    // პასუხისმგებელია სესხების მართვაზე.
    public class LoanService 
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IUserDataManager _userRepository;


        public LoanService(
         ILoanRepository loanRepository,
         IUserDataManager userRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
        }


        // ქმნის ახალი სესხის მოთხოვნას.
        public void RequestLoan(
            int userId,
            decimal amount)
        {

            if (amount <= 0)
                throw new InvalidAmountException("Amount must be greater than 0");

            if (amount > 10000)
                throw new InvalidAmountException("Loan limit exceeded");
            LoanRequest loan =
            new LoanRequest
            {
                Id =
                _loanRepository.GetAll().Count + 1,

                UserId = userId,

                Amount = amount,
                Status = LoanStatus.Pending
            };


            _loanRepository.Add(loan);
        }
        public List<LoanRequest> GetLoans()
        {
            return _loanRepository.GetAll();
        }
        // უარყოფს სესხის მოთხოვნას.
        public void RejectLoan(int id)
        {
            var loans = _loanRepository.GetAll();

            var loan = loans.FirstOrDefault(x => x.Id == id);


            if (loan == null)
                throw new LoanNotFoundException();


            loan.Status = LoanStatus.Rejected;


            _loanRepository.Save(loans);
        }
        // ამტკიცებს სესხს და თანხას ურიცხავს მომხმარებელს.
        public void ApproveLoan(int id)
        {

            var loans = _loanRepository.GetAll();

            var loan = loans.FirstOrDefault(x => x.Id == id);


            if (loan == null)
                throw new LoanNotFoundException();


            var user =
            _userRepository.GetUserById(loan.UserId);


            if (user == null)
                throw new UserNotFoundException();

            if (user is ClientUser client)
            {
                client.Deposit(loan.Amount);
                _userRepository.UpdateUser(client);
            }
            else
            {
                throw new Exception("User is not client");
            }


            loan.Status = LoanStatus.Approved;


          

            _loanRepository.Save(loans);
        }
    }
}
