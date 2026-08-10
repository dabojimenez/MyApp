using Application.DTOs.Persons;
using Domain;
using Domain.Abstractions;

namespace Application.UseCases.Persons
{
    public class UpdatePersonUseCase
    {
        private readonly IRepository<PersonEntity, Guid> _repository;
        
        public UpdatePersonUseCase(
            IRepository<PersonEntity, Guid> repository
            )
        {
            _repository = repository;
        }
        public async Task<PersonEntity> ExecuteAsync(UpdatePersonDto dto)
        {
            PersonEntity? person = await _repository.GetByIdAsync(dto.Id);
            if (person is null)
                throw new InvalidOperationException($"No se encontro a la persona con el id: {dto.Id}");

            person.UpdatePersonalInfo(dto.FirstName, dto.LastName, dto.Email, dto.PhoneNumber);

            await _repository.UpdateAsync(person);
            await _repository.SaveChangesAsync();

            return person;
        }
    }
}
