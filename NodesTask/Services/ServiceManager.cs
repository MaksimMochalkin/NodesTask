namespace NodesTask.Services
{
    using NodesTask.Data;
    using NodesTask.Interfaces;

    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ITreeService> _treeService;
        private readonly Lazy<INodeService> _nodeService;
        private readonly Lazy<IExceptionJournalService> _exceptionJournalService;

        public ServiceManager(NodesApplicationDbContext context)
        {
            _treeService = new Lazy<ITreeService>(() => new TreeService(context));
            _nodeService = new Lazy<INodeService>(() => new NodeService(context));
            _exceptionJournalService = new Lazy<IExceptionJournalService>(() => new ExceptionJournalService(context));
        }

        public ITreeService TreeService => _treeService.Value;
        public INodeService NodeService => _nodeService.Value;
        public IExceptionJournalService ExceptionJournalService => _exceptionJournalService.Value;
    }
}
