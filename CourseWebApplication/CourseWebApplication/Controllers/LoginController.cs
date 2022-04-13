using Microsoft.AspNetCore.Mvc;
using WebApp.Jwt;
using WebApp.Models;
using WebApp.Models.Requests;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model not valid");

            string error = Errors.HasAnyErrors(loginRequest.Login, loginRequest.Password);
            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);

            var result = await _tokenService.Authenticate(loginRequest);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
