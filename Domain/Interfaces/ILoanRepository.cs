using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ILoanRepository
    {
        List<LoanRequest> GetAll();
        void Add(LoanRequest loan);
        void Save(List<LoanRequest> loans);
    }
}
