using System.Text.RegularExpressions;

namespace Domain
{
    // Una entidad: es un elemento que le da forma al dominio y trabaja con la lógica de negocio. Una entidad tiene una identidad única y puede tener propiedades y
    // comportamientos asociados. En este caso, la entidad PersonEntity representa a una persona en el dominio de la aplicación.
    public class PersonEntity
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";


        public PersonEntity(
            string code
            , string firstName
            , string lastName
            , string email
            , string phoneNumber
            )
        {
            ValidateCode(code);
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidateEmail(email);
            ValidatePhoneNumber(phoneNumber);

            Id = Guid.NewGuid();
            Code = code.Trim().ToUpper();
            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber.Trim();
        }

        public void UpdatePersonalInfo(
            string firstName
            , string lastName
            , string email
            , string phoneNumber
            )
        {
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidateEmail(email);
            ValidatePhoneNumber(phoneNumber);

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Email = email.Trim();
            PhoneNumber = phoneNumber.Trim();
        }

        // Reglas del negocio: Son las reglas que definen cómo se comporta la entidad y cómo interactúa con otras entidades. En este caso, la regla de negocio es que el
        // código de la persona debe ser único y no puede estar vacío.
        private void ValidateCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                throw new ArgumentException("El codigo no puede estar vacío", nameof(code));

            // Otra regla, seria que el codigo no debe ser menor a tres caracteres
            if (code.Length < 3)
                throw new ArgumentException("El codigo no puede ser menor a tres caracteres", nameof(code));

            // otra regla, que no sea mayor a 20 caracteres
            if (code.Length > 20)
                throw new ArgumentException("El codigo no puede ser mayor a veinte caracteres", nameof(code));
        }

        private void ValidateFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("El nombre no puede estar vacío", nameof(firstName));
            if (firstName.Length < 2)
                throw new ArgumentException("El nombre no puede ser menor a dos caracteres", nameof(firstName));
            if (firstName.Length > 50)
                throw new ArgumentException("El nombre no puede ser mayor a cincuenta caracteres", nameof(firstName));
        }

        private void ValidateLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("El apellido no puede estar vacío", nameof(lastName));
            if (lastName.Length < 2)
                throw new ArgumentException("El apellido no puede ser menor a dos caracteres", nameof(lastName));
            if (lastName.Length > 50)
                throw new ArgumentException("El apellido no puede ser mayor a cincuenta caracteres", nameof(lastName));
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("El correo electrónico no puede estar vacío", nameof(email));
            if (email.Length < 5)
                throw new ArgumentException("El correo electrónico no puede ser menor a cinco caracteres", nameof(email));
            if (email.Length > 250)
                throw new ArgumentException("El correo electrónico no puede ser mayor a doscientos cincuenta caracteres", nameof(email));

            string EMAIL_PATTERN = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            if (!Regex.IsMatch(email, EMAIL_PATTERN))
                throw new ArgumentException("El correo electrónico no es válido", nameof(email));
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException("El número de teléfono no puede estar vacío", nameof(phoneNumber));
            if (phoneNumber.Length < 7)
                throw new ArgumentException("El número de teléfono no puede ser menor a siete caracteres", nameof(phoneNumber));
            if (phoneNumber.Length > 15)
                throw new ArgumentException("El número de teléfono no puede ser mayor a quince caracteres", nameof(phoneNumber));
            string PHONE_PATTERN = @"^\+?[0-9]{7,15}$";
            if (!Regex.IsMatch(phoneNumber, PHONE_PATTERN))
                throw new ArgumentException("El número de teléfono no es válido", nameof(phoneNumber));
        }
    }
}
