namespace NodesTask.Interfaces
{
    using NodesTask.Data.Entities;

    public interface INodeService
    {
        Task<Tree> CreateNodeAsync(string treeName, Guid? parentNodeId, string nodeName);
        Task DeleteNodeAsync(string treeName, Guid nodeId);
        Task RenameNodeAsync(string treeName, Guid nodeId, string newNodeName);
    }
}
