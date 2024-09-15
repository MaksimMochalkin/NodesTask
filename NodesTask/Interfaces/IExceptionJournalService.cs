namespace NodesTask.Interfaces
{
    using NodesTask.Data.Entities;

    public interface IExceptionJournalService
    {
        Task<IEnumerable<ExceptionJournal>> GetJournalEntriesAsync(int skip, int take, DateTime? from, DateTime? to, string search);
        Task<ExceptionJournal> GetJournalEntryByIdAsync(Guid id);
        Task<ExceptionJournal> LogExceptionAsync(string exceptionType, string queryParams, string bodyParams, string stackTrace);
    }
}
