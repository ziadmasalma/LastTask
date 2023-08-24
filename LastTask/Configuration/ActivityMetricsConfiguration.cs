using LastTask.Table;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LastTask.Configuration
{
    public class ActivityMetricsConfiguration : IEntityTypeConfiguration<ActivityMetrics>
    {
        public void Configure(EntityTypeBuilder<ActivityMetrics> builder)
        {
            builder.HasKey(a => a.UserId);

            builder.Property(a => a.NumberOfPosts)
                .IsRequired();

            builder.Property(a => a.NumberOfFollowers)
                .IsRequired();

            builder.Property(a => a.NumberOfFollowing)
                .IsRequired();

            builder.HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<ActivityMetrics>(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
