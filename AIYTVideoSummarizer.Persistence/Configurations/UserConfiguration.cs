
using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIYTVideoSummarizer.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(254);

            builder.Property(u => u.PasswordHash)
                   .HasMaxLength(500);
            
            builder.Property(u => u.Salt)
                .HasMaxLength(128);

            builder.Property(u => u.IsEmailConfirmed)
                .HasDefaultValue(false);

            builder.Property(u => u.EmailConfirmationToken)
                .HasMaxLength(256);

            builder.Property(u => u.PasswordResetToken)
                .HasMaxLength(256);

            builder.Property(u => u.PasswordResetTokenExpiry)
                .HasColumnType("timestamp with time zone");

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder.Property(u => u.FirstName)
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                   .HasMaxLength(100);

            builder.Property(u => u.ProfilePictureUrl)
                   .HasMaxLength(700);

            builder.Property(u => u.Bio)
                   .HasMaxLength(1000);

            builder.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(u => u.UserName)
                .IsUnique();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasMany(u => u.ExternalLogins)
                .WithOne(ex => ex.User)
                .HasForeignKey(ex => ex.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Summaries)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(u => u.SummarizationRequests)
                .WithOne(sr => sr.User)
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
