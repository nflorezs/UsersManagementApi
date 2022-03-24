using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Swashbuckle.AspNetCore.Annotations;
using Transversals;
using Transversals.Filters;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userServices)
        {
            _userService = userServices;
        }

        /// <summary>
        /// Gets all users from Db
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/Users",Name = "GetUsers")]
        [ProducesResponseType(typeof(Response<IEnumerable<DatumDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, 5);
            var response = await _userService.GetUsers(validFilter);
            return Ok(response);
        }

        /// <summary>
        /// Gets a specific user searched by Id in the Bd
        /// </summary>
        /// <param name="id">user identifier</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("/api/Users/{id}", Name = "GetUsersById")]
        [ProducesResponseType(typeof(Response<DatumDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersById(int id)
        {
            var response = await _userService.GetUserById(id);
            return Ok(response);
        }

        /// <summary>
        /// Create a new user validating required fields
        /// </summary>
        /// <param name="UserData"></param>
        /// <returns></returns>
        [HttpPost("/api/Users", Name = "CreateUser")]
        [ProducesResponseType(typeof(Response<DatumDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser(DatumDto UserData)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.CreateUser(UserData);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update user information in db
        /// </summary>
        /// <param name="UserData"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("/api/Users/{id}", Name = "UpdateUser")]
        [ProducesResponseType(typeof(Response<DatumDto>), StatusCodes.Status200OK)]

        public async Task<IActionResult> UpdateUser(DatumDto UserData, int id)
        {
            if (ModelState.IsValid)
            {
                UserData.id = id;
                var response = await _userService.UpdateUser(UserData);
                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}