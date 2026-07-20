using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Application.Services
{
    public class EmailService
    {
        public void SeeEmail(string to, string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("lizaalizaa558@gmail.com", "ybek emvd ogxy uiha");
                smtpClient.EnableSsl = true;

                MailMessage mailMessage = new MailMessage("lizaalizaa558@gmail.com", to, subject, body);

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new EmailSendingException("Email sending failed: " + ex.Message);
            }
        }
    }
}