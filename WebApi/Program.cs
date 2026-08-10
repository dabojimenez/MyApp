using Application.UseCases.Persons;
using Data;
using Data.Repositories;
using Domain;
using Domain.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Te hace falta la conexión a la base de datos");

//builder.Services.AddScoped<IRepository<PersonEntity, Guid>, PersonRepository>();
//builder.Services.AddScoped<ICodeRepository<PersonEntity>, PersonRepository>();
builder.Services.AddData(connectionString);

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