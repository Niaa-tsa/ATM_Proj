using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public  class ValidationHelper
    {
        public  bool IsPositive(decimal amount)
        {
            return amount > 0;
        }
    }
}
