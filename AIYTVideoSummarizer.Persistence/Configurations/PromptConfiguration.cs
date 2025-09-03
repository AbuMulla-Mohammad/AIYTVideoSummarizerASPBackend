
using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIYTVideoSummarizer.Persistence.Configurations
{
    public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
    {
        public void Configure(EntityTypeBuilder<Prompt> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(p => p.Text)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasMany(p => p.SummarizationRequests)
                .WithOne(sr => sr.Prompt)
                .HasForeignKey(sr => sr.PromptId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Summaries)
                .WithOne(s => s.Prompt)
                .HasForeignKey(s => s.PromptId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
