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
