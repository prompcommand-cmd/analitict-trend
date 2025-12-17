using AnaliticTrend.Application.Abstracts.UseCases.Production.Trend;
using AnaliticTrend.Application.Models.Auth.Response;
using AnaliticTrend.Application.Models.Dto;
using AnaliticTrend.Application.Models.Production.Trend.Request;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using WebApi.Abstracts;
using WebApi.Models;
using WebApi.Utilities;

namespace AnaliticTrend.WebApi.EndPoints.Production.Trend
{
    public class OrderTrendEndPoint : IEndpoint
    {
        private static string urlFragment = "production";

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            RouteGroupBuilder group = app.MapGroup($"{urlFragment}")
                .WithTags("Production")
                .AddFluentValidationAutoValidation();

            group.MapPost("trend", async Task<IResult> ([FromBody] OrderTrendRequest request,
                IOrderTrendUseCase _orderTrendUseCase) =>
            {
                var result = await _orderTrendUseCase.GetListOrderTrend(request).ConfigureAwait(false);
                return Results.Ok(result.ToStandardResponse());
            })
            .WithMetadata()
            .Produces<ResponseBuilder<List<OrderRecordDto>>>(StatusCodes.Status200OK)
            .Produces<ErrorDetail>(StatusCodes.Status400BadRequest);
        }
    }
}
