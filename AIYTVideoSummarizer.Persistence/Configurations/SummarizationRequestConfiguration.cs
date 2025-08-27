using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIYTVideoSummarizer.Persistence.Configurations
{
    public class SummarizationRequestConfiguration : IEntityTypeConfiguration<SummarizationRequest>
    {
        public void Configure(EntityTypeBuilder<SummarizationRequest> builder)
        {
            builder.Property(sr => sr.RequestedAt)
                .IsRequired()
                .HasDefaultValueSql("NOW()");

            builder.Property(sr => sr.RequestStatus)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(70)
                .HasDefaultValue(RequestStatus.Pending);

            builder.Property(sr => sr.ErrorMessage)
                .HasMaxLength(1000);

            builder.HasIndex(sr => sr.UserId);

            builder.HasIndex(sr => sr.VideoId);

            builder.HasIndex(sr => sr.PromptId);

            builder.HasOne(sr => sr.User)
                .WithMany(u => u.SummarizationRequests)
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sr => sr.Video)
                .WithMany(v => v.SummarizationRequest)
                .HasForeignKey(sr => sr.VideoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(sr => sr.Prompt)
                .WithMany(p => p.SummarizationRequests)
                .HasForeignKey(sr => sr.PromptId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
