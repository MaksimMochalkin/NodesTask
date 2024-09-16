namespace NodesTask.Services
{
    using Microsoft.EntityFrameworkCore;
    using NodesTask.Data;
    using NodesTask.Data.Entities;
    using NodesTask.Interfaces;
    using NodesTask.Models.Requests;

    public class ExceptionJournalService : IExceptionJournalService
    {
        private readonly ISqlQueryBuilderService<ExceptionJournal> _sqlQueryBuilderService;
        private readonly NodesApplicationDbContext _context;

        public ExceptionJournalService(NodesApplicationDbContext context,
            ISqlQueryBuilderService<ExceptionJournal> sqlQueryBuilderService)
        {
            _context = context;
            _sqlQueryBuilderService = sqlQueryBuilderService;
        }

        public async Task<IEnumerable<ExceptionJournal>> GetJournalEntriesAsync(int skip, int take, FilterParams filter)
        {
            var query = _context.ExceptionJournals.AsQueryable();

            if (filter.From != DateTime.MinValue && filter.To != DateTime.MinValue)
            {
                query = query.Where(e => e.Timestamp >= filter.From && e.Timestamp <= filter.To);
            }

            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = _sqlQueryBuilderService.GenerateSqlQuery(query, filter.Search);
            }

            query = query.Skip(skip).Take(take);

            var result = await query.ToListAsync();
            return result;
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
