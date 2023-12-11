using CryproServices.Users.Infrastructure.Models;
using CryproServices.Users.Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryproServices.Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        protected readonly IUsersService _service;
        protected readonly IJWTService _jwtservice;
        protected readonly IConfiguration _configurationManager;

        public UsersController(IUsersService service, IJWTService jwtservice, IConfiguration configurationManager)
        {
            _service = service;
            _jwtservice = jwtservice;
            _configurationManager = configurationManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            var user = await _service.GetUserByLoginAndPassword(model);
            if (user != null)
            {
                var salt = _configurationManager.GetSection("JWT")["secretKey"];
                var token = _jwtservice.GenerateJwtToken(user.Name, salt);

                return Ok(new LoggedUserModel()
                {
                    User = user,
                    Token = token
                });
            }

            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllUsers());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUser(NewUserModel user)
        {
            return Ok(await _service.AddUser(user));
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _service.RemoveUser(id));
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserModel user)
        {
            return Ok(await _service.UpdateUser(user));
        }
    }
}
