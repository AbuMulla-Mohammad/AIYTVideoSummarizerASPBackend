
using AIYTVideoSummarizer.Application.DTOs.PromptDtos;
using AIYTVideoSummarizer.Domain.Common.Models.PaginationModels;
using MediatR;

namespace AIYTVideoSummarizer.Application.Queries.PromptQueries
{
    public class GetAllPromptsQuery:IRequest<PaginatedList<PromptListDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchQuery { get; set; }
    }
}
