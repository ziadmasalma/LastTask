using BCrypt.Net;
using LastTask.Model;
using LastTask.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LastTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _UserService;
        public UserController(IUserService userService)
        {
            _UserService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post(UserRigModel m)
        {
            if (m.password != m.confpassword)
                return BadRequest("Password Not Matched");
           
            var res = _UserService.AddUser(m);
            return Ok(res);

        }
        [HttpPost("login")]
        public async Task<IActionResult> login(Userlogin u)
        {
            var user = await _UserService.GetUser(u.username);
            if (user == null)
            {
                return BadRequest("user dose not exist");
            }
            if (!BCrypt.Net.BCrypt.Verify(u.password, user.PasswordHash))
            {
                return BadRequest("wrong password");
            }
            var token = _UserService.CreateToken(u.username);
            return Ok(token);
        }
    }
}
