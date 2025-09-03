
using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIYTVideoSummarizer.Persistence.Configurations
{
    public class UserExternalLoginConfiguration : IEntityTypeConfiguration<UserExternalLogin>
    {
        public void Configure(EntityTypeBuilder<UserExternalLogin> builder)
        {
            builder.Property(ex => ex.LoginProvider)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ex => ex.ProviderKey)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(ex => new { ex.LoginProvider, ex.ProviderKey })
                .IsUnique();

            builder.HasOne(ux => ux.User)
                .WithMany(u => u.ExternalLogins)
                .HasForeignKey(ux => ux.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
