using Application.Models;
using Application.Utilities;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utilities;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;

namespace WebApi.Extension
{
    public class FluentValidationCustomResultFactory : IFluentValidationAutoValidationResultFactory
    {
        public IResult CreateResult(EndpointFilterInvocationContext context, ValidationResult validationResult)
        {
            var listMessage = validationResult.Errors
                .Select(s => s.ErrorMessage)
                .ToList();
            return Results.BadRequest(new ProblemDetails { }.ToStandardResponse(new ErrorDetail
            {
                IsError = true,
                ErrorMessages = listMessage,
                ErrorType = CustomErrorType.Validation.ToDescription()
            }));
        }
    }
}
