using Application.UseCases.Persons;
using Data;
using WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Te hace falta la conexión a la base de datos");

//builder.Services.AddScoped<IRepository<PersonEntity, Guid>, PersonRepository>();
//builder.Services.AddScoped<ICodeRepository<PersonEntity>, PersonRepository>();
builder.Services.AddData(connectionString);
// Ejecutar este comando, para poder usar las migraciones, junto al paquete nuget, de Design
// dotnet tool install --global dotnet-ef

// Para crear una migraciona
// dotnet ef migrations add InitialCreate --project Data --startup-project WebApi
// InitialCreate: nombre de la migracion
// --project: flag, que nos permite indentificar el proyecto donde esta EFCore, en este caso es el proyecto Data
// --startup-project: flag, que nos pemrite identificar el proyecto que tiene la cadena de conexion, en este caso es WebApi

// Para usar o aplicar las migraciones en la base de datos
// dotnet ef database update --project Data --startup-project WebApi

//Comandos utilizados:

//Instalación de dotnet - ef de manrea global, necesario para ejecutar comandos de Entity Framework:
//dotnet tool install--global dotnet-ef
//Creación de migración inicial:
//dotnet ef migrations add InitialCreate --project Data --startup-project WebApi

//Aplicar migraciones pendientes a la base de datos:
//dotnet ef database update --project Data --startup-project WebApi

builder.Services.AddScoped<CreatePersonUseCase>();
builder.Services.AddScoped<GetPersonByIdUseCase>();
builder.Services.AddScoped<GetPersonByCodeUseCase>();
builder.Services.AddScoped<GetAllPersonsUseCase>();
builder.Services.AddScoped<UpdatePersonUseCase>();
builder.Services.AddScoped<DeletePersonUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPersonsEndpoints();

app.Run();

//string name = "Juan";
//Console.WriteLine(name.Hi());

//static class StringExtensions
//{
//    public static string Hi(this string str)
//    {
//        return "Hola " + str;
//    }
//}