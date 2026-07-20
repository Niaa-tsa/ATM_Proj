using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class UserNotFoundException : AppException
    {
        public UserNotFoundException()
            : base("User not found") { }
    }
}
