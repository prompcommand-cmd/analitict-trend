using WebApi.Abstracts;
using WebApi.Models;

namespace WebApi.EndPoints
{
    public class HealthEndPoint : IEndpoint
    {
        private static string urlFragment = "health";

        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"/{urlFragment}", () =>
            {
                return Results.Ok(new ResponseBase
                {
                    status = StatusCodes.Status200OK,
                    message = "API is healthy"
                });
            })
            .AllowAnonymous()
            .WithTags("Health")
            .Produces<ResponseBase>(StatusCodes.Status200OK);
        }
    }
}
