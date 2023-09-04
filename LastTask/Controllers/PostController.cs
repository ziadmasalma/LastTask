using LastTask.Model;
using LastTask.Service.Post;
using LastTask.Service.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LastTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        public readonly IPostService _postService;
        public readonly IUserService _userService;
        public PostController(IPostService postService , IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> addPost(PostModel postModel)
        {
            var post =await _postService.addPost(_userService.GetCurrentLoggedIn().Value, postModel);
            return Ok(post);
        }
    }
}
