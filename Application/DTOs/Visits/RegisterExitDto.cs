namespace Application.DTOs.Visits
{
    public class RegisterExitDto
    {
        public Guid? VisitId { get; set; }
        public string? Code { get; set; }
        public DateTime? ExitTime { get; set; }
    }
}
