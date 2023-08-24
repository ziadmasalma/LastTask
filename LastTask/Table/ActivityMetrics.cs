using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LastTask.Table
{
    public class ActivityMetrics
    {
        [Key]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public int NumberOfPosts { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfFollowing { get; set; }

        public User User { get; set; }
    }
}
