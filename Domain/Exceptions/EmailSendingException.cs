using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class EmailSendingException : AppException
    {
        public EmailSendingException(string message)
            : base(message) { }
    }
}