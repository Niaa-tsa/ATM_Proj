using Domain.Models;
using Domain.Interfaces;
using Infrastructure;
using Domain.Exceptions;
namespace Application.Services
{
    public class LoanService 
    {
        private readonly LoanRepository _loanRepository;
        private readonly IUserDataManager _userRepository;


        public LoanService(
            LoanRepository loanRepository,
            IUserDataManager userRepository)
        {
            _loanRepository = loanRepository;
            _userRepository = userRepository;
        }



        public void RequestLoan(
            int userId,
            decimal amount)
        {

            if (amount <= 0)
                throw new InvalidAmountException("Amount must be greater than 0");


            LoanRequest loan =
            new LoanRequest
            {
                Id =
                _loanRepository.GetAll().Count + 1,

                UserId = userId,

                Amount = amount,

                Status = "Pending"
            };


            _loanRepository.Add(loan);
        }
        public List<LoanRequest> GetLoans()
        {
            return _loanRepository.GetAll();
        }
        public void RejectLoan(int id)
        {
            var loans = _loanRepository.GetAll();

            var loan = loans.FirstOrDefault(x => x.Id == id);


            if (loan == null)
                throw new Exception("Loan not found");


            loan.Status = "Rejected";


            _loanRepository.Save(loans);
        }
        public void ApproveLoan(int id)
        {

            var loans = _loanRepository.GetAll();

            var loan = loans.FirstOrDefault(x => x.Id == id);


            if (loan == null)
                throw new Exception("Loan not found");


            var user =
            _userRepository.GetUserById(loan.UserId);


            if (user == null)
                throw new Exception("User not found");


            user.Balance += loan.Amount;


            loan.Status = "Approved";


            _userRepository.UpdateUser(user);

            _loanRepository.Save(loans);
        }
    }
}
