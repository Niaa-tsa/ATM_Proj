using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    internal class InvalidAmountException : Exception
    {
        public InvalidAmountException(string message) : base(message) { }
    }
}
