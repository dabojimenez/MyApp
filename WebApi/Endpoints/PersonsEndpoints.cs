using Application.DTOs.Persons;
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

            group.MapPost("/", async (CreatePersonDto dto, CreatePersonUseCase useCase) =>
            {
                try
                {
                    PersonEntity person = await useCase.ExecuteAsync(dto);
                    return Results.Created($"/api/persons/{person.Id}", person);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (ArgumentException ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(new { error = ex.Message });
                }
            }).WithName("CreatePerson")
            .WithSummary("Crea una nueva persona")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError)
            ;
        }
    }
}
