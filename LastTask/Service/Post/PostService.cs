using LastTask.Model;
using LastTask.Table;
using Microsoft.EntityFrameworkCore;

namespace LastTask.Service.Post
{
    public class PostService:IPostService
    {
        public readonly AplicationDbContext _context;
        public PostService(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Table.Post> addPost(int userId,PostModel post)
        {
            var postTable = new Table.Post {
                UserId = userId,
                Content = post.content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _context.Posts.AddAsync(postTable);
            await _context.SaveChangesAsync();
            return postTable;

        }
        public async Task<bool> EditPost(int postId, int userId, PostModel postModel)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null || post.UserId != userId)
            {
                return false;
            }

            post.Content = postModel.content;
            post.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePost(int postId, int userId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null || post.UserId != userId)
            {
                return false;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LikePost(int postId, int userId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null)
            {
                return false;
            }

            var like = new Like
            {
                UserId = userId,
                PostId = postId
            };

            _context.Likes.Add(like);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CommentOnPost(int postId, int userId, CommentModel commentModel)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null)
            {
                return false;
            }

            var comment = new Comment
            {
                UserId = userId,
                PostId = postId,
                Content = commentModel.Content,
                CreatedAt = DateTime.Now,
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return true;
        }

        
            public async Task<(int LikesCount, int CommentsCount)> GetPostMetrics(int postId)
            {
                var post = await _context.Posts
                    .Include(p => p.Likes) // Include the Likes collection
                    .Include(p => p.Comments) // Include the Comments collection
                    .FirstOrDefaultAsync(p => p.PostId == postId);

                if (post == null)
                {
                    return (-1, -1); // Return -1 for both counts to indicate that the post doesn't exist
                }

                var likesCount = post.Likes.Count;
                var commentsCount = post.Comments.Count;

                return (likesCount, commentsCount);
            }
        
    }
}
