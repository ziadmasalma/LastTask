using LastTask.Model;

namespace LastTask.Service.Post
{
    public interface IPostService
    {
        public Task<Table.Post> addPost(int userId, PostModel post);
    }
}
