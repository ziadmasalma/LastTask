using LastTask.Model;

namespace LastTask.Service.Post
{
    public class PostService:IPostService
    {
        public readonly AplicationDbContext _Context;
        public PostService(AplicationDbContext context)
        {
            _Context = context;
        }

        public async Task<Table.Post> addPost(int userId,PostModel post)
        {
            var postTable = new Table.Post {
                UserId = userId,
                Content = post.content,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _Context.Posts.AddAsync(postTable);
            await _Context.SaveChangesAsync();
            return postTable;

        }
    }
}
