using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class InvalidCredentialsException : AppException
    {
        public InvalidCredentialsException()
            : base("Invalid email or password or not verified") { }
    }
}