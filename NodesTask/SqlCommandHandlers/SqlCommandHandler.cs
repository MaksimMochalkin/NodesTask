namespace NodesTask.SqlCommandHandlers
{
    using NodesTask.Interfaces;

    public abstract class SqlCommandHandler<T> : ISqlCommandHandler<T> where T : class
    {
        public virtual string Command => "unknown";

        public abstract IQueryable<T> Execute(IQueryable<T> query, string propertyName, string value, bool descending);
    }
}
