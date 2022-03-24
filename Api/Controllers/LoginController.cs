using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Transversals;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Generate Token for auth user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("api/Authenticate")]
        [ProducesResponseType(typeof(Response<AuthenticateResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate(AuthenticateRequestDto model)
        {
            var response = await _userService.Authenticate(model);

            if (response.Data == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        /// <summary>
        /// Generate username and password for an user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("api/GenerateCredentials/{id}")]
        [ProducesResponseType(typeof(Response<DatumLoginDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateCredentials(int id)
        {
            return Ok(await _userService.GenerateCredentials(id));
        }
    }
}
