using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    namespace Application.Interfaces
    {
        public interface ILoanService
        {
            void RequestLoan(int userId, decimal amount);

            List<LoanRequest> GetLoans();

            void ApproveLoan(int id);

            void RejectLoan(int id);
        }
    }
}