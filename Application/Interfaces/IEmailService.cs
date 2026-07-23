using System;
using System.Collections.Generic;
using System.Text;

using System;

namespace Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(
            string to,
            string subject,
            string body
        );


        void SendVerificationEmail(
            string email,
            string verificationCode
        );
    }
}