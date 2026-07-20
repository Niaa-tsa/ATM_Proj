using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public abstract class AppException : Exception
    {
        public AppException(string message) : base(message) { }
    }
}