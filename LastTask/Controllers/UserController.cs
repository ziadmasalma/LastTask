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
            var token = _UserService.AddUser(m);
            return Ok(token);

        }
    }
}
