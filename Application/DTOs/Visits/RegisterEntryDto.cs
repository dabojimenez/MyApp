namespace Application.DTOs.Visits
{
    public class RegisterEntryDto
    {
        public Guid? PersonId { get; set; }
        public string? Code { get; set; }
        public DateTime? EntryTime { get; set; }
    }
}
