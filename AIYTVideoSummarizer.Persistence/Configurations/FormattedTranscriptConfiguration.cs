
using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIYTVideoSummarizer.Persistence.Configurations
{
    public class FormattedTranscriptConfiguration : IEntityTypeConfiguration<FormattedTranscript>
    {
        public void Configure(EntityTypeBuilder<FormattedTranscript> builder)
        {
            builder.Property(ft => ft.Text)
                .IsRequired();

            builder.HasIndex(ft => ft.VideoId);

            builder.HasOne(ft => ft.Video)
                .WithMany(v => v.FormattedTranscripts)
                .HasForeignKey(ft => ft.VideoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
