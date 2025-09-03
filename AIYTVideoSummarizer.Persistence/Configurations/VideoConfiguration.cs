using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIYTVideoSummarizer.Persistence.Configurations
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.Property(v => v.YouTubeVideoID)
                .IsRequired()
                .HasMaxLength(11);


            builder.Property(v => v.YouTubeUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(v => v.Title)
                .HasMaxLength(100);

            builder.Property(v => v.ChannelName)
                .HasMaxLength(50);


            builder.HasMany(v => v.Summaries)
                .WithOne(s => s.Video)
                .HasForeignKey(s => s.VideoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.SummarizationRequest)
                .WithOne(sr => sr.Video)
                .HasForeignKey(sr => sr.VideoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(v => v.FormattedTranscripts)
                .WithOne(ft => ft.Video)
                .HasForeignKey(ft => ft.VideoId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
