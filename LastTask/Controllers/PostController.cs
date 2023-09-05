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
        [HttpPut("{postId}")]
        public async Task<IActionResult> EditPost(int postId, PostModel postModel)
        {
            var userId = _userService.GetCurrentLoggedIn().Value;
            var result = await _postService.EditPost(postId, userId, postModel);

            if (result)
            {
                return Ok("Post edited successfully.");
            }
            return NotFound("Post not found or you do not have permission to edit it.");
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var userId = _userService.GetCurrentLoggedIn().Value; 
            var result = await _postService.DeletePost(postId, userId);

            if (result)
            {
                return Ok("Post deleted successfully.");
            }
            return NotFound("Post not found or you do not have permission to delete it.");
        }

        [HttpPost("{postId}/like")]
        public async Task<IActionResult> LikePost(int postId)
        {
            var userId = _userService.GetCurrentLoggedIn().Value; 
            var result = await _postService.LikePost(postId, userId);

            if (result)
            {
                return Ok("Post liked successfully.");
            }
            return BadRequest("Unable to like the post.");
        }

        [HttpPost("{postId}/comment")]
        public async Task<IActionResult> CommentOnPost(int postId, CommentModel commentModel)
        {
            var userId = _userService.GetCurrentLoggedIn().Value; 
            var result = await _postService.CommentOnPost(postId, userId, commentModel);

            if (result)
            {
                return Ok("Comment added successfully.");
            }
            return BadRequest("Unable to add the comment.");
        }

        [HttpGet("{postId}/metrics")]
        public async Task<IActionResult> GetPostMetrics(int postId)
        {
            var metrics = await _postService.GetPostMetrics(postId);
            return Ok(metrics);
        }
        [HttpGet("posts")]
        public async Task<IActionResult> getposts()
        {
            var result = await _postService.getallPost();
            List<string> strings = new List<string>();
            foreach (var post in result)
            {
                strings.Add(post.Content.ToString());
            }
            return Ok(strings);
        }
    }
}
