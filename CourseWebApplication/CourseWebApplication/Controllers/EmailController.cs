
using Microsoft.AspNetCore.Mvc;
using WebApp.IService;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        const string subject = "Код для регистрации в приложение";

        private readonly IConfiguration _configuration;

        private readonly IEmailService _emailService;

        public MailController(IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(SendMessageRequest to)
        {
            string login = _configuration["Data:Login"];
            string password = _configuration["Data:Password"];

            int code = await _emailService.SendEmail(login, to.Email, password, subject);

            return Ok(new SendMessageReponse() { Code = code });
        }
    }
}
