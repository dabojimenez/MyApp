using Data.Persistence;
using Data.Repositories;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    // Clase que se encargara, de encapsular todos los elementos que se vayna  inyectr en otro componenten, sobre el componente Data
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(
            this IServiceCollection services
            , string connectionString
            )
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IRepository<PersonEntity, Guid>, PersonRepository>();
            services.AddScoped<ICodeRepository<PersonEntity>, PersonRepository>();

            return services;
        }
    }
}
