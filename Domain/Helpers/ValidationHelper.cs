using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsPositive(decimal amount)
        {
            return amount > 0;
        }
    }
}
