using System.ComponentModel.DataAnnotations;

namespace LastTask.Table
{
    public class Profile
    {
        [Key] // Assuming UserId is the primary key and foreign key to User
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
        public string ProfileImageURL { get; set; }
        public User User { get; set; }
    }
}
