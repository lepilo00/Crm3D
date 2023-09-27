using Crm3D.Models;
using System.Net;
using System.Net.Mail;

namespace Crm3D.Services
{
    public class SendEmailService : ISendEmailService
    {
        public async Task SendEmailAsync(User user)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("4test7.47@gmail.com");
            message.To.Add(user.Email);
            message.Subject = "Prijava na online CPP";
            message.Body = $"Pozdraljeni {user.Name}!\n\n Obvescamo vas, da ste prijavljeni na online CPP predavanja v " +
                $"avtosoli 3D.\nVasi osebni podatki so:\n" +
                $"{user.Name}\n" +
                $"{user.Surname}\n" +
                $"{user.Email}\n" +
                $"Zahvaljujemo se vam za vaso izbiro!!\n" +
                $"Hvala in lep pozdrav\n" +
                $"Ekipa 3D";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential("4test7.47@gmail.com", "trra vxju cqek uszq");
            smtpClient.EnableSsl = true;

            try
            {
                smtpClient.Send(message);
                Console.WriteLine("Email Sent Successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
