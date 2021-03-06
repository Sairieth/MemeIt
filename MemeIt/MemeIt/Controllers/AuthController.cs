using System.Net;
using MemeIt.Core;
using MemeIt.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using MemeIt.Infrastructure;
using Newtonsoft.Json;

namespace MemeIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthData>> Login([FromBody] LoginData credentials)
        {
            if (!ModelState.IsValid) return BadRequest("missing passowrd/username");

            var user = await _userRepository.GetUserByUsernameAsync(credentials.Username);
            if (user == null) return BadRequest(new {error = "invalid username/password"});

            var passwordValid = _authService.VerifyPassword(credentials.Password, user.HashedPassword);
            if (!passwordValid) return BadRequest(new {error = "invalid username/password"});

            var authData = _authService.GetAuthData(user.Id, user.Role, user.Username,credentials.Offset);

            Response.Cookies.Append("JWT", authData.Token,
                new CookieOptions() {Expires = authData.TokenExpirationTime, MaxAge = authData.TokenExpirationTime.TimeOfDay, IsEssential = true});


            //Console.WriteLine(DateTime.UtcNow);
            //Console.WriteLine(authData.TokenExpirationTime);
            //Console.WriteLine(authData.TokenExpirationTime.TimeOfDay);
            //foreach (var cookiesKey in Request.Cookies.Keys)
            //{
            //    Console.WriteLine(cookiesKey);
            //}

            return Accepted(new {authData.Token});
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthData>> Register([FromBody] RegisterUserDto registerData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var emailUniq = await _userRepository.IsEmailUniqueAsync(registerData.Email);
            if (!emailUniq) return BadRequest(new {error = "user with this email already exists"});

            var usernameUniq = await _userRepository.IsUsernameUniqueAsync(registerData.UserName);
            if (!usernameUniq) return BadRequest(new {error = "user with this email already exists"});

            var user = registerData.RegisterUserDtoToUser();

            await _userRepository.AddUserAsync(user);

            var authData = _authService.GetAuthData(user.Id, user.Role, user.Username,registerData.Offset);

            Response.Cookies.Append("JWT", authData.Token,
                new CookieOptions() { Expires = authData.TokenExpirationTime, MaxAge = authData.TokenExpirationTime.TimeOfDay, IsEssential = true });

            return Created("/api/auth/register", new { authData.Token });
        }
    }
}