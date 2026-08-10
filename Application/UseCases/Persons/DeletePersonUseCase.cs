using Domain;
using Domain.Abstractions;

namespace Application.UseCases.Persons
{
    public class DeletePersonUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;

        public DeletePersonUseCase(
            IRepository<PersonEntity, Guid> repository
            )
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var person = await _repository.GetByIdAsync( id );
            if (person is null)
                throw new InvalidOperationException($"No se encontro a la persona con el id: {id}");

            await _repository.DeleteAsync( person );
            await _repository.SaveChangesAsync();
        }
    }
}
