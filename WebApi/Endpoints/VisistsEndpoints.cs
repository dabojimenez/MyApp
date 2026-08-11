namespace WebApi.Endpoints
{
    public static class VisistsEndpoints
    {
        public static void MapVisitsEndpoints(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder? group = app.MapGroup("/api/visits")
                .WithTags("Visits");
        }
    }
}
