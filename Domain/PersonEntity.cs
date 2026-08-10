namespace Domain
{
    // Una entidad: es un elemento que le da forma al dominio y trabaja con la lógica de negocio. Una entidad tiene una identidad única y puede tener propiedades y
    // comportamientos asociados. En este caso, la entidad PersonEntity representa a una persona en el dominio de la aplicación.
    public class PersonEntity
    {
        public Guid Id { get; private set; }
        public string Code { get;private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get;private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;

        public PersonEntity(
            string code
            , string firstName
            , string lastName
            , string email
            , string phoneNumber
            )
        {
            Id = Guid.NewGuid();
            Code = code.Trim().ToUpper();
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber.Trim();
        }
    }
}
