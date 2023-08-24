using LastTask.Configuration;
using LastTask.Table;
using Microsoft.EntityFrameworkCore;

namespace LastTask
{
   
        public class AplicationDbContext : DbContext
        {
            public AplicationDbContext(DbContextOptions<AplicationDbContext> option) : base(option)
            {

            }

            public DbSet<User> Users { get; set; }
            public DbSet<Post> Posts { get; set; }
            public DbSet<Like> Likes { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Friendship> Friendships { get; set; }
            public DbSet<ActivityMetrics> ActivityMetrics { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                 modelBuilder.ApplyConfiguration(new UserConfiguration());
                 modelBuilder.ApplyConfiguration(new  PostConfiguration());
                 modelBuilder.ApplyConfiguration(new  LikeConfiguration());
                 modelBuilder.ApplyConfiguration(new CommentConfiguration());
                 modelBuilder.ApplyConfiguration(new  FriendshipConfiguration());
                 modelBuilder.ApplyConfiguration(new ActivityMetricsConfiguration());



            }
        }
    }

