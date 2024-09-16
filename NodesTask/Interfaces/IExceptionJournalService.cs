namespace NodesTask.Interfaces
{
    using NodesTask.Data.Entities;
    using NodesTask.Models.Requests;

    public interface IExceptionJournalService
    {
        Task<IEnumerable<ExceptionJournal>> GetJournalEntriesAsync(int skip, int take, FilterParams filter);
        Task<ExceptionJournal> GetJournalEntryByIdAsync(Guid id);
        Task<ExceptionJournal> LogExceptionAsync(string exceptionType, string queryParams, string bodyParams, string stackTrace);
    }
}
