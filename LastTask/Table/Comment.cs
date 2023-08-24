namespace LastTask.Table
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}
