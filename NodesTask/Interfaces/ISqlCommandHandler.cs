namespace NodesTask.Interfaces
{
    public interface ISqlCommandHandler<T> where T : class
    {
        string Command { get; }

        IQueryable<T> Execute(IQueryable<T> query, string propertyName, string value, bool descending);

    }
}
