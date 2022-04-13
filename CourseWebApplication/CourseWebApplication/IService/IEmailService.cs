namespace WebApp.IService
{
    public interface IEmailService
    {
        Task<int> SendEmail(string from, string to, string password, string subject);
    }
}
