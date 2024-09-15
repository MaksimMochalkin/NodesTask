namespace NodesTask.Services
{
    using Microsoft.EntityFrameworkCore;
    using NodesTask.Data;
    using NodesTask.Data.Entities;
    using NodesTask.Interfaces;

    public class ExceptionJournalService : IExceptionJournalService
    {
        private readonly NodesApplicationDbContext _context;

        public ExceptionJournalService(NodesApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExceptionJournal>> GetJournalEntriesAsync(int skip, int take, DateTime? from, DateTime? to, string search)
        {
            var query = _context.ExceptionJournals.AsQueryable();

            if (from.HasValue)
                query = query.Where(e => e.Timestamp >= from.Value);
            if (to.HasValue)
                query = query.Where(e => e.Timestamp <= to.Value);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(e => e.StackTrace.Contains(search) || e.QueryParams.Contains(search) || e.BodyParams.Contains(search));

            return await query.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<ExceptionJournal> GetJournalEntryByIdAsync(Guid id)
        {
            return await _context.ExceptionJournals.FindAsync(id);
        }

        public async Task<ExceptionJournal> LogExceptionAsync(string exceptionType, string queryParams, string bodyParams, string stackTrace)
        {
            var journalEntry = new ExceptionJournal
            {
                EventId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                ExceptionType = exceptionType,
                QueryParams = queryParams,
                BodyParams = bodyParams,
                StackTrace = stackTrace
            };

            _context.ExceptionJournals.Add(journalEntry);
            await _context.SaveChangesAsync();

            return journalEntry;
        }
    }
}
