

namespace AIYTVideoSummarizer.Application.DTOs.VideoDtos
{
    public class CreateVideoDto
    {
        public string YouTubeVideoID { get; set; } = string.Empty;
        public string YouTubeUrl { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? ChannelName { get; set; }
    }
}
