using Application.DTOs.Visits;
using Application.UseCases.Visits;
using Domain;

namespace WebApi.Endpoints
{
    public static class VisistsEndpoints
    {
        public static void MapVisitsEndpoints(this IEndpointRouteBuilder app)
        {
            RouteGroupBuilder? group = app.MapGroup("/api/visits")
                .WithTags("Visits");

            group.MapGet("/", async (GetAllVisitsUseCase useCase) =>
            {
                try
                {
                    var visits = await useCase.ExecuteAsync();
                    return Results.Ok(visits);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(new { error = ex.Message });
                }
            }).WithName("GetAllVisits")
            .WithSummary("Obtener todas las visitas")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            ;

            group.MapPost("/entry", async (RegisterEntryDto dto, RegisterEntryUseCase useCase) =>
            {
                try
                {
                    VisitEntity? visit = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/visits/{visit.Id}", visit);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(new { error = ex.Message });
                }
            }).WithName("RegisterEntry")
            .WithSummary("Registra entrada de una persona (por Personid o code)")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            ;

            group.MapGet("/active", async (GetActiveVisitsUseCase useCase) =>
            {
                try
                {
                    var visits = await useCase.ExecuteAsync();
                    return Results.Ok(visits);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(new { error = ex.Message });
                }
            }).WithName("GetActivesVisits")
            .WithSummary("Obtener todas las visitas activas (personas dentro)")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            ;

        }
    }
}
