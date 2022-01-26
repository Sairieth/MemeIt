using MemeIt.Core;
using MemeIt.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Route("/login")]
        public async Task<ActionResult<AuthData>> Login(LoginData credentials)
        {
            if (!ModelState.IsValid) return BadRequest("missing passowrd/username");

            var user = await _userRepository.GetUserByUsernameAsync(credentials.Username);

            if (user == null)
            {
                return BadRequest(new { error = "invalid username/password" });
            }

            var passwordValid = _authService.VerifyPassword(credentials.Password, user.HashedPassword);
            if (!passwordValid)
            {
                return BadRequest(new { error = "invalid username/password" });
            }

            return _authService.GetAuthData(user.Id, user.Role.Title!);

        }
    }
}
