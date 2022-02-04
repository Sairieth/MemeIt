using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MemeIt.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MemeIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPut]
        [Route("{userId:long}/edit-email")]
        public async Task<ActionResult> EditEmail([FromRoute] long userId,[FromBody]string newEmail)
        {
            var user = await _userRepository.GetUserAsync(userId);
            if (user == null) return BadRequest("No user found");

            await _userRepository.EditEmailAsync(userId, newEmail);

            return Ok("ok");
        }

        [HttpPut]
        [Route("{userId:long}/edit-password")]
        public async Task<ActionResult> EditPassword([FromRoute] long userId,[FromBody]string newPass)
        {
            var user = await _userRepository.GetUserAsync(userId);
            if (user == null) return BadRequest("No user found by id");

            await _userRepository.EditPasswordAsync(userId, newPass);

            return Ok("ok");
        }

        [HttpDelete]
        [Route("{userId:long}/delete")]
        public async Task<ActionResult> DeleteAccount([FromRoute] long userId)
        {
            var storedToken = Request.Headers.Authorization.First().Split(" ")[1];
            var valid = _authService.IsValidId(storedToken, userId);

            if (!valid) return BadRequest();

            var user = await _userRepository.GetUserAsync(userId);

            if (user == null) return BadRequest("No user found by id");

            await _userRepository.RemoveUserAsync(userId);

            return Ok("removed");
        }
    }
}
