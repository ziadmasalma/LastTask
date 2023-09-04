
    using LastTask.Table;
    using System;
    using System.ComponentModel.DataAnnotations;

namespace LastTask.Model
{
        public class CommentModel
        {
            public int CommentId { get; set; }

            [Required]
            public int UserId { get; set; } // ID of the user who posted the comment

            [Required]
            public int PostId { get; set; } // ID of the post the comment is associated with

            [Required]
            [MaxLength(500)] // Adjust the maximum length as needed
            public string Content { get; set; } // Comment content

            [Required]
            public DateTime CreatedAt { get; set; }

            
            // Navigation properties
            public User User { get; set; } // User who posted the comment
            public Post Post { get; set; } // Post the comment is associated with
        }
    }

