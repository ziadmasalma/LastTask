namespace LastTask.Table
{
    public enum FriendshipStatus
    {
        Pending,
        Accepted,
        Declined
    }

    public class Friendship
        {
            public int FriendshipId { get; set; }
            public int UserId { get; set; }
            public int FriendId { get; set; }
            public FriendshipStatus Status { get; set; }
            public DateTime CreatedAt { get; set; }

            public User User { get; set; }
            public User Friend { get; set; }
        }
    
}
