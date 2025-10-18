using AIYTVideoSummarizer.Domain.Common.Exceptions;
using System.Net;
using AIYTVideoSummarizer.Api.Common.Responses;
using FluentValidation;

namespace AIYTVideoSummarizer.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode;
            object apiResponse;

            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    apiResponse = ApiResponse<string>.FailResponse(notFoundException.Message);
                    break;

                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    var errors = validationException.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    apiResponse = ApiResponse<object>.FailResponse(
                        "Validation failed",
                        errors
                        );
                    break;

                case InvalidYouTubeUrlException invalidYouTubeUrlException:
                    statusCode = HttpStatusCode.BadRequest;
                    apiResponse = ApiResponse<string>.FailResponse(invalidYouTubeUrlException.Message);
                    break;

                case SummarizationFailedException summarizationFailedException:
                    statusCode = HttpStatusCode.UnprocessableEntity;
                    apiResponse = ApiResponse<string>.FailResponse(summarizationFailedException.Message);
                    break;

                case ExternalAIServiceException externalAIServiceException:
                    statusCode = HttpStatusCode.ServiceUnavailable;
                    apiResponse = ApiResponse<string>.FailResponse(externalAIServiceException.Message);
                    break;

                case ConflictException conflictException:
                    statusCode = HttpStatusCode.Conflict;
                    apiResponse = ApiResponse<string>.FailResponse(conflictException.Message);
                    break;

                case InvalidCredentialsException invalidCredentialsException:
                    statusCode = HttpStatusCode.Unauthorized;
                    apiResponse = ApiResponse<string>.FailResponse(invalidCredentialsException.Message);
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    apiResponse = ApiResponse<string>.FailResponse("An unexpected error occurred.");
                    break;
            }
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(apiResponse);
        }
    }
}
