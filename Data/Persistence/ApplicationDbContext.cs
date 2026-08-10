using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
            ) : base(options)
        {

        }

        public DbSet<PersonEntity> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Especificamos la configuración de la entidad PersonEntity
            modelBuilder.Entity<PersonEntity>(entity =>
            {
                entity.ToTable("Persons");

                entity.HasKey(p => p.Id); // Definimos la clave primaria

                entity.Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedOnAdd(); // Configuramos la propiedad Id como clave primaria y autogenerada

                entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20);

                // Especificacion de indices
                entity.Property(e => e.Code)
                .IsUnicode(); // Indicamos que sea un indice y que sea unico


                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(250);
                entity.Property(p => p.PhoneNumber).HasMaxLength(15);

                entity.Ignore(e => e.FullName); // Ignoramos la propiedad fullName ya que es una propiedad calculada y no se almacena en la base de datos

                // Campos extras, que no estan en la entidad, pero que se necesitan para la persistencia de datos
                entity.Property<DateTime>("CreatedAt")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

                entity.Property<DateTime?>("UpdatedAt")
                .HasDefaultValueSql("GETUTCDATE()");
            });
        }

        // Sobreescribimos este metodo, para poner configuracones extras que se aplicaran en todas las consultas de la base de datos, como por ejemplo, el uso
        // de filtros globales, para que no se traigan registros eliminados logicos
        // Funcionaria como un interceptor de todas las consultas que se hagan a la base de datos, para poder aplicar reglas de negocio, como por ejemplo, que no
        // se traigan registros eliminados logicos
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
