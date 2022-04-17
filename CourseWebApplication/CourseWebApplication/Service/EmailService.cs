using MimeKit;
using MailKit.Net.Smtp;
using WebApp.IService;

namespace WebApp.Models
{
    public class EmailService:IEmailService
    {
        public async Task<int> SendEmail(string from, string to, string password, string subject)
        {
            int code = Generator.GetRandom();
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Отправитель: ", from));
                emailMessage.To.Add(new MailboxAddress("Получатель: ", to));
                emailMessage.Subject = subject;
                
                emailMessage.Body = new TextPart("plain")
                {
                    Text = "Ваш код для регистрации: " + code
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587);
                    await client.AuthenticateAsync(from, password);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch(Exception ex)
            {
                code = -1;
            }
            return code;
        }
    }

    static class Generator
    {
        public static int GetRandom()
        {
            Random rnd = new Random();
            int value = rnd.Next(1000, 10000);
            return value;
        }
    }

}
