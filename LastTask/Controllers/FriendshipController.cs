using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LastTask.Table; // Import your table models
using LastTask.Service.User;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace LastTask.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly UserService _userService;

        public FriendshipController(AplicationDbContext context,UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // Send a friend request
        [HttpPost("send-request")]
        public async Task<IActionResult> SendFriendRequest( int friendId)
        {
            var friendship = new Friendship
            {
                UserId = _userService.GetCurrentLoggedIn().Value,
                FriendId = friendId,
                Status = FriendshipStatus.Pending,
                CreatedAt = DateTime.Now
            };

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return Ok("Friend request sent.");
        }

        // Accept a friend request
        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptFriendRequest(int friendshipId)
        {
            var friendship = await _context.Friendships.FindAsync(friendshipId);

            if (friendship == null)
            {
                return NotFound("Friendship not found.");
            }

            friendship.Status = FriendshipStatus.Accepted;
            await _context.SaveChangesAsync();

            return Ok("Friend request accepted.");
        }

        // Reject a friend request
        [HttpPost("reject-request")]
        public async Task<IActionResult> RejectFriendRequest(int friendshipId)
        {
            var friendship = await _context.Friendships.FindAsync(friendshipId);

            if (friendship == null)
            {
                return NotFound("Friendship not found.");
            }

            friendship.Status = FriendshipStatus.Declined;
            await _context.SaveChangesAsync();

            return Ok("Friend request rejected.");
        }

        // Get a user's friend list
        [HttpGet("friend-list")]
        public async Task<IActionResult> GetFriendList()
        {
            var userId = _userService.GetCurrentLoggedIn().Value;
            var friendList = await _context.Friendships
                .Where(f => (f.UserId == userId || f.FriendId == userId) && f.Status == FriendshipStatus.Accepted)
                .Select(f => f.UserId == userId ? f.Friend : f.User)
                .ToListAsync();

            return Ok(friendList);
        }
    }
}
