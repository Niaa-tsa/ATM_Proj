using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class LoanNotFoundException : AppException
    {
        public LoanNotFoundException()
            : base("Loan not found") { }
    }
}
