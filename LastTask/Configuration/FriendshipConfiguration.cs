using LastTask.Table;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LastTask.Configuration
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(f => f.FriendshipId);

            builder.Property(f => f.Status)
                .IsRequired();

            builder.Property(f => f.CreatedAt)
                .IsRequired();

            builder.HasOne(f => f.User)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

           
        }
    }
}
