using Data.Persistence;
using Domain;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PersonRepository : IRepository<PersonEntity, Guid>
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(
            ApplicationDbContext context
            )
        {
            _context = context;
        }

        public async Task<PersonEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Persons
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PersonEntity>> GetAllAsync()
        {
            return await _context.Persons
                .AsNoTracking() // No tendra un rastreo de cambios, lo que mejora el rendimiento para consultas de SOLO lectura
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .ToListAsync();
        }

        public async Task AddAsync(PersonEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _context.Persons
                .AddAsync(entity);
        }

        public Task UpdateAsync(PersonEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Persons
                .Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(PersonEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Persons.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
