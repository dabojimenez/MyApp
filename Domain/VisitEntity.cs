namespace Domain
{
    public class VisitEntity
    {
        public Guid Id { get; private set; }
        public Guid PersonId { get; private set; }
        public DateTime EntryTime { get; private set; }
        public DateTime? ExitTime { get; private set; }
        public PersonEntity? Person { get; private set; }

        // Porpiedades informativas
        public bool isActive => ExitTime is null;
        public TimeSpan? Duration => ExitTime.HasValue ? ExitTime.Value - EntryTime : null;

        private VisitEntity() { }

        public VisitEntity(Guid personId, DateTime? entryTime = null)
        {
            if (personId == Guid.Empty)
                throw new ArgumentException("El Id de la persona no puede ser vacío", nameof(personId));

            Id = Guid.NewGuid();
            PersonId = personId;
            EntryTime = entryTime ?? DateTime.UtcNow;
            ExitTime = null;
        }

        public void RegisterExit(DateTime? exitTime = null)
        {
            DateTime exit = exitTime ?? DateTime.UtcNow;

            if (ExitTime.HasValue)
                throw new InvalidOperationException("Esta visita ya tiene una salida registrada");

            if (exit <= EntryTime)
                throw new ArgumentException("La hora de salida debe ser posterior a la hora de entrada", nameof(exitTime));

            ExitTime = exit;
        }
    }
}
