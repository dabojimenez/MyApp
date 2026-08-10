namespace Domain.Abstractions
{
    public interface IVisitRepository<TEntiy> where TEntiy : class
    {
        Task<bool> HasActiveVisitAsync(Guid personid);

        Task<IEnumerable<TEntiy>> GetActiveVisitAsync();

        Task<IEnumerable<TEntiy>> GetVisitsByPersonIdAsync(Guid personId);
    }
}
