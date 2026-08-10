using Domain;
using Domain.Abstractions;

namespace Application.UseCases.Persons
{
    public class GetPersonByIdUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;
        public GetPersonByIdUseCase(
            IRepository<PersonEntity, Guid> repository
            )
        {
            _repository = repository;
        }
        public async Task<PersonEntity> ExecuteAsync(Guid id)
        {
            PersonEntity? person = await _repository.GetByIdAsync(id);
            if (person is null)
                throw new InvalidOperationException($"No se encontro una persona con el id {id}");
            return person;
        }
    }
}
