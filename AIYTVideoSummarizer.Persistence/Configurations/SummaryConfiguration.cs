
using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIYTVideoSummarizer.Persistence.Configurations
{
    public class SummaryConfiguration : IEntityTypeConfiguration<Summary>
    {
        public void Configure(EntityTypeBuilder<Summary> builder)
        {

            builder.Property(s => s.CreatedAt)
                   .IsRequired()
                   .HasDefaultValueSql("NOW()");

            builder.HasIndex(s => s.VideoId);
            builder.HasIndex(s => s.PromptId);
            builder.HasIndex(s => s.UserId);


            builder.HasMany(s => s.SummarySections)
                .WithOne(ss => ss.Summary)
                .HasForeignKey(ss => ss.SummaryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Video)
                .WithMany(v => v.Summaries)
                .HasForeignKey(s => s.VideoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Prompt)
                .WithMany(p => p.Summaries)
                .HasForeignKey(s => s.PromptId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Summaries)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
