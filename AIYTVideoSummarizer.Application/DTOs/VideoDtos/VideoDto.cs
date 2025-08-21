
namespace AIYTVideoSummarizer.Application.DTOs.VideoDtos
{
    public class VideoDto
    {
        public Guid VideoId { get; set; }
        public string YouTubeVideoID { get; set; } = string.Empty;
        public string YouTubeUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? ChannelName { get; set; }
    }
}
