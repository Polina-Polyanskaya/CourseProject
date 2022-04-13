using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Jwt;
using WebApp.Models;
using WebApp.Models.folder;
using WebApp.Models.Requests;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IHttpContextAccessor contextAccessor, IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterRequest user)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid");

            string error = Errors.HasAnyErrors(user.Login, user.Password);
            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);

            var model = _mapper.Map<User>(user);
            error = await _userService.AddUser(model);

            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest updateRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            string error = Errors.HasAnyErrors(updateRequest.Password);
            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);

            string? userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return BadRequest();

            User user = await _userService.GetUserById(Guid.Parse((ReadOnlySpan<char>)userId));

            if (user is null)
                return NotFound("No such user");

            if(BcryptPasswordHasher.VerifyPassword(updateRequest.Password,user.Password))
                return BadRequest("Wrong password");

            _mapper.Map(updateRequest, user);

            error = await _userService.UpdateUser(user);

            if (error is not null)
                return BadRequest();

            return Ok();
        }

        [HttpPost("checkUser")]
        [AllowAnonymous]
        public async Task<ActionResult> CheckUser(CheckUserRequest user)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            bool exists = await _userService.CheckUser(user);
            return Ok(new CheckUserResponse() { Exists = exists }) ;
        }

    }
}
