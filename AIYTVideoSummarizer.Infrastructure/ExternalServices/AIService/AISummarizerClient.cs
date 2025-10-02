
using AIYTVideoSummarizer.Application.DTOs.VideoDtos;
using AIYTVideoSummarizer.Application.Interfaces.ExternalServices;
using AIYTVideoSummarizer.Domain.Common.Exceptions;
using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace AIYTVideoSummarizer.Infrastructure.ExternalServices.AIService
{
    public class AISummarizerClient : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<AISummarizerOptions> _options;

        public AISummarizerClient(
            HttpClient httpClient,
            IOptions<AISummarizerOptions> options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        public async Task<string> GetYTVideoId(string videoUrl)
        {
            var url = $"{_options.Value.BaseUrl}{_options.Value.GetVideoIdEndpoint}?url={videoUrl}";
            var response = await _httpClient.PostAsync(url,content:null);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidYouTubeUrlException("Invalid YouTube Url.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Dictionary<string, string>>(content);
            return result?["video_id"] ?? content;
        }

        public async Task<VideoSummaryResponseDto> SummarizeVideo(string videoUrl, Guid promptId)
        {
            var url = $"{_options.Value.BaseUrl}{_options.Value.SummarizeEndpoint}";

            var requestBody = new
            {
                video_url = videoUrl,
            };
            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

            }catch(HttpRequestException)
            {
                throw new ExternalAIServiceException("The AI service is unavailable. Please try again later.");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var videoSummaryResponse = JsonSerializer.Deserialize<VideoSummaryResponseDto>(responseJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (videoSummaryResponse == null)
                throw new InvalidOperationException($"Failed to deserialize response: {responseJson}");

            return videoSummaryResponse;
        }
    }
}
