using LastTask.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LastTask.Configuration
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(p => p.UserId);
            builder.Property(p => p.FullName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Bio).HasMaxLength(250).IsRequired();
            builder.Property(p => p.ProfileImageURL).HasMaxLength(200).IsRequired();

            // Configure one-to-one relationship with User
            builder.HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId);
        }
    }
}
