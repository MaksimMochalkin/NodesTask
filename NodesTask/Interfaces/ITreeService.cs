namespace NodesTask.Interfaces
{
    using NodesTask.Data.Entities;

    public interface ITreeService
    {
        Task<Tree> GetTreeByNameAsync(string treeName);
        Task<Tree> GetTreeWithNodesAsync(string treeName);
        Task CreateTreeAsync(Tree tree);
    }
}
