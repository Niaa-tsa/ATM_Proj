using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class UserAlreadyExistsException : AppException
    {
        public UserAlreadyExistsException()
            : base("User already exists") { }
    }
}