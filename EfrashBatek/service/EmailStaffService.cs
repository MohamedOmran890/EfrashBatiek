using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EfrashBatek.service
{
    public class EmailStaffService
    {
        public async Task SendEmail(string email,string UserName,String Password,string Name)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("omran942487@gmail.com", "ecnbupilijwjsnuw");
                int otpValue = new Random().Next(100000, 999999);
                var fromAddress = new MailAddress("omran942487@gmail.com", "Efrash Batek");
                var toAddress = new MailAddress(email);
                const string subject = "Your login details for MyWebsite";
                var body = $"Hello {Name},<br/><br/>Your login details for MyWebsite are:<br/>Username: {UserName}<br/>Password: {Password}";

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
