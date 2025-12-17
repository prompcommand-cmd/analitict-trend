using Application.Exceptions;
using Application.Models;
using Application.Utilities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using WebApi.Models;
using WebApi.Utilities;

namespace WebApi.Middleware
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is UnauthorizedAccessException unauthorizedException)
            {
                var problemDetails = new ProblemDetails { }.ToStandardResponse(new ErrorDetail
                {
                    IsError = true,
                    ErrorType = CustomErrorType.Unauthenticated.ToDescription(),
                    ErrorMessages = new List<string>() { unauthorizedException.Message }
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response
                    .WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }

            // more specific handling error
            // E.g. can return list/object ErrorData to be processed
            if (exception is CustomValidationException validationException)
            {
                var problemDetails = new ProblemDetails { }.ToStandardResponse(new ErrorDetail
                {
                    IsError = true,
                    ErrorMessages = validationException.ValidationResult.ErrorMessage?.Split("\n").ToList(),
                    ErrorData = validationException.Value
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                await httpContext.Response
                    .WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }

            if (exception is CustomValidationException400 validationException400)
            {
                var problemDetails = new ProblemDetails { }.ToStandardResponse(new ErrorDetail
                {
                    IsError = true,
                    ErrorType = CustomErrorType.Validation.ToDescription(),
                    ErrorCategory = validationException400.ErrorCategory,
                    ErrorMessages = validationException400.ValidationResult.ErrorMessage?.Split("\n").ToList(),
                    ErrorData = validationException400.Value
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response
                    .WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }

            // already handle error in FE generally
            if (exception is DataException dataException)
            {
                var problemDetails = new ProblemDetails { }.ToStandardResponse(new ErrorDetail
                {
                    IsError = true,
                    ErrorType = "VALIDATION",
                    ErrorMessages = new List<string>() { dataException.Message }
                });

                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response
                    .WriteAsJsonAsync(problemDetails, cancellationToken);

                return true;
            }
            return false;
        }
    }
}
