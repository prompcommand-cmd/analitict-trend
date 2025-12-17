using Core.Constant.Message;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IHostEnvironment _environment;

        public GlobalExceptionHandler(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var traceId = LogIdGenerator.GenerateTimestampedRandomId();
            using (var reader = new StreamReader(httpContext.Request.Body))
            {
                // Reset the stream position to the beginning after enabling buffering
                httpContext.Request.Body.Seek(0, SeekOrigin.Begin);

                // Read the request body as a string
                var requestBody = await reader.ReadToEndAsync();

                // Process the request body as needed
                Log
                .ForContext("UserId", httpContext.Items["UserName"])
                .ForContext("TraceIdentifier", traceId)
                .ForContext("Body", requestBody)
                .Error(exception, GetResponseMessage(httpContext));

            }

            var problemDetails = new ProblemDetails { }.ToStandardResponse(new ErrorDetail
            {
                IsError = true,
                ErrorId = traceId,
                ErrorCategory = ErrorCategories.SystemError,
                ErrorType = GetResponseMessage(httpContext),
                ErrorMessages = _environment.IsProduction() ? null : new List<string> { exception.Message }
            });

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static string GetResponseMessage(HttpContext context)
        {
            string responseBody;
            switch (context.Response.StatusCode)
            {
                case StatusCodes.Status204NoContent:
                case StatusCodes.Status400BadRequest:
                case StatusCodes.Status401Unauthorized:
                case StatusCodes.Status403Forbidden:
                case StatusCodes.Status404NotFound:
                case StatusCodes.Status500InternalServerError:
                case StatusCodes.Status501NotImplemented:
                    responseBody = errorMessages[context.Response.StatusCode];
                    break;
                default:
                    responseBody = "Unknown errors.";
                    break;

            }
            return responseBody;
        }

        private static readonly Dictionary<int, string> errorMessages = new Dictionary<int, string>
        {
            { StatusCodes.Status204NoContent, "No content." },
            { StatusCodes.Status400BadRequest, "Bad request." },
            { StatusCodes.Status401Unauthorized, "Unauthorized access." },
            { StatusCodes.Status403Forbidden, "Forbidden access." },
            { StatusCodes.Status404NotFound, "Resource not found." },
            { StatusCodes.Status500InternalServerError, "Internal server error." },
            { StatusCodes.Status501NotImplemented, "Not implemented." }
        };
    }
}
