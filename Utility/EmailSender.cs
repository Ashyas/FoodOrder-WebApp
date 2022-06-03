using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class EmailSender : IEmailSender
    {
        /*private readonly IEmailSender _emailSender;
        public EmailSender(IEmailSender emailSender)
        {
                _emailSender = emailSender;
        }*/

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("ash.app.test274@gmail.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            //sending email
           
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com",587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("ash.app.test274@gmail.com", "!t$@tes7AccountDn7try");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            };
            return Task.CompletedTask;
        }
    }
}
