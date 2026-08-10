using Domain;
using Domain.Abstractions;

namespace Application.UseCases.Persons
{
    public class GetPersonByCodeUseCase
    {
        private readonly ICodeRepository<PersonEntity> _repository;
        public GetPersonByCodeUseCase(
            ICodeRepository<PersonEntity> repository
            )
        {
            _repository = repository;
        }
        public async Task<PersonEntity> ExecuteAsync(string code)
        {
            PersonEntity? person = await _repository.GetByCodeAsync(code);
            if (person is null)
                throw new InvalidOperationException($"No se encontro una persona con el codigo {code}");
            return person;
        }
    }
}
