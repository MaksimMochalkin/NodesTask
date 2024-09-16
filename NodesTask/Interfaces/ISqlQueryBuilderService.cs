namespace NodesTask.Interfaces
{
    public interface ISqlQueryBuilderService<T> where T : class
    {
        IQueryable<T> GenerateSqlQuery(IQueryable<T> query, string filter);
    }
}
