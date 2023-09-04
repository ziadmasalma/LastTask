using BCrypt.Net;
using LastTask.Model;
using LastTask.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost("Auth")]
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
            var token = _UserService.CreateToken(user);
            _UserService.setsessionvalue(user);
            return Ok(token);
        }
        [HttpPost("createprofile")]
        public async Task<IActionResult> createprofile(UserModel model)
        {

            var userIdClaim = _UserService.GetCurrentLoggedIn();
            if (userIdClaim == null)
            {
                return BadRequest("UserId claim is missing in the token.");
            }

            
            var profile = await _UserService.createProfile(userIdClaim.Value ,model);
            return Ok(profile);
        }
        

    }
}
