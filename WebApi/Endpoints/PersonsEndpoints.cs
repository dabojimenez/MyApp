using Application.UseCases.Persons;
using Domain;

namespace WebApi.Endpoints
{
    public static class PersonsEndpoints
    {
        public static void MapPersonsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/persons")
                .WithTags("Persons");

            group.MapGet("/{id:guid}", async (Guid id, GetPersonByIdUseCase useCase) =>
            {
                try
                {
                    PersonEntity person = await useCase.ExecuteAsync(id);
                    return Results.Ok(person);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new {error = ex.Message});
                }
            }).WithName("GetPersonById")
            .WithSummary("Obtener una persona por su Id")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
