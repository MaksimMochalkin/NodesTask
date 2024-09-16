namespace NodesTask.Services
{
    using NodesTask.Interfaces;
    using NodesTask.SqlCommandHandlers;
    using System.Linq;

    public class SqlQueryBuilderService<T> : ISqlQueryBuilderService<T> where T : class
    {
        private static Dictionary<string, ISqlCommandHandler<T>> _sqlCommandHandlers => InitCommandHandlers();

        public SqlQueryBuilderService()
        {
        }

        private static Dictionary<string, ISqlCommandHandler<T>> InitCommandHandlers()
        {
            return new Dictionary<string, ISqlCommandHandler<T>>
            {
                { "equal", new EqualSqlCommandHandler<T>() },
                { "notequal", new NotEqualSqlCommandHandler<T>() },
                { "like", new LikeSqlCommandHandler<T>() },
                { "orderby", new OrderBySqlCommandHandler<T>() }
            };
        }

        public IQueryable<T> GenerateSqlQuery(IQueryable<T> query, string filter)
        {
            var commands = filter.Split(',');

            foreach (var command in commands)
            {
                var parts = command.Split(':');
                if (parts.Length < 3)
                {
                    continue;
                }

                var operation = parts[0].Trim().ToLower();
                var columnName = parts[1].Trim();
                var value = parts[2].Trim();

                if (string.IsNullOrWhiteSpace(operation) ||
                    string.IsNullOrWhiteSpace(columnName) ||
                    string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                query = ApplyFilter(query, columnName, operation, value);
            }

            return query;
        }

        private IQueryable<T> ApplyFilter(IQueryable<T> query, string columnName, string operation, string value, bool descending = false)
        {
            if (!_sqlCommandHandlers.TryGetValue(operation.ToLower(), out var sqlCommandHandler))
            {
                throw new NotImplementedException("Sql handler not found");
            }

            var finalQuery = sqlCommandHandler.Execute(query, columnName, value, descending);

            return finalQuery;
        }
    }
}
