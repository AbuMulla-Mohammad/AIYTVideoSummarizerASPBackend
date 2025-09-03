
using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIYTVideoSummarizer.Persistence.Configurations
{
    public class SummarySectionConfiguration : IEntityTypeConfiguration<SummarySection>
    {
        public void Configure(EntityTypeBuilder<SummarySection> builder)
        {
            builder.Property(ss => ss.Title)
                .HasMaxLength(200);

            builder.Property(ss => ss.Text)
                .IsRequired();

            builder.HasIndex(ss => ss.SummaryId);

            builder.HasOne(ss => ss.Summary)
                .WithMany(s => s.SummarySections)
                .HasForeignKey(ss => ss.SummaryId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
