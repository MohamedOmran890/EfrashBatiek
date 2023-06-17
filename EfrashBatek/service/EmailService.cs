using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EfrashBatek.service
{
    public class EmailService
    {
        public async Task SendConfirmationEmail(string email, string confirmationLink)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("omran942487@gmail.com", "ecnbupilijwjsnuw");
                int otpValue = new Random().Next(100000, 999999);
                var fromAddress = new MailAddress("omran942487@gmail.com", "Efrash Batek");
                var toAddress = new MailAddress(email);
                const string subject = "Confirm your email address";
                var body = $"Please click the following link to confirm your email address: {confirmationLink}";

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                await client.SendMailAsync(message);

            }
        }
        public async Task SendForgetPassword(string email, string confirmationLink)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("omran942487@gmail.com", "ecnbupilijwjsnuw");
                int otpValue = new Random().Next(100000, 999999);
                var fromAddress = new MailAddress("omran942487@gmail.com", "Efrash Batek");
                var toAddress = new MailAddress(email);
                const string subject = "Forget Password";
                var body = $"Please reset your password by clicking here: {confirmationLink}";

                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                await client.SendMailAsync(message);

            }
        }

    }
}
