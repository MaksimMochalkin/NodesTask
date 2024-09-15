namespace NodesTask.Interfaces
{
    public interface IServiceManager
    {
        ITreeService TreeService { get; }
        INodeService NodeService { get; }
        IExceptionJournalService ExceptionJournalService { get; }
    }
}
