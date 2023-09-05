using LastTask.Model;

namespace LastTask.Service.Post
{
    public interface IPostService
    {
        public Task<Table.Post> addPost(int userId, PostModel post);
        Task<bool> EditPost(int postId, int userId, PostModel postModel);
        Task<bool> DeletePost(int postId, int userId);
        Task<bool> LikePost(int postId, int userId);
        Task<bool> CommentOnPost(int postId, int userId, CommentModel commentModel);
        Task<(int LikesCount, int CommentsCount)> GetPostMetrics(int postId);
        Task<List<Table.Post>> getallPost();

    }
}
