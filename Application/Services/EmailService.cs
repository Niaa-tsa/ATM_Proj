using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        // აგზავნის ელფოსტას SMTP-ის გამოყენებით.
        public void SendEmail(string to, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new NetworkCredential(
                "lizaalizaa558@gmail.com",
                "ybek emvd ogxy uiha"
            );

            smtpClient.EnableSsl = true;


            MailMessage message = new MailMessage();

            message.From = new MailAddress(
                "lizaalizaa558@gmail.com"
            );

            message.To.Add(to);

            message.Subject = subject;

            message.Body = body;



            message.IsBodyHtml = true;


            smtpClient.Send(message);
        }
public void SendVerificationEmail(
    string email,
    string verificationCode)
        {

            string htmlBody = $@"
<!DOCTYPE html>
<html>

<body style='font-family: Arial;'>


<div style='background:#f4f6f8;padding:30px;'>


<h1 style='color:#4F46E5;'>
Welcome to ATM System!
</h1>


<p>
Your verification code is:
</p>


<h2 style='color:#4F46E5;'>
{verificationCode}
</h2>


<p>
Thank you for registering.
</p>


</div>


</body>

</html>
";


            SendEmail(
                email,
                "ATM System Verification Code",
                htmlBody
            );
        }
    }
    }
