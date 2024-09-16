namespace NodesTask.Services
{
    using Microsoft.EntityFrameworkCore;
    using NodesTask.Data;
    using NodesTask.Data.Entities;
    using NodesTask.Interfaces;
    using NodesTask.Models.Exceptions;

    public class NodeService : INodeService
    {
        private readonly NodesApplicationDbContext _context;

        public NodeService(NodesApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tree> CreateNodeAsync(
            string treeName,
            Guid? parentNodeId,
            string nodeName)
        {
            var tree = await _context.Trees.Include(t => t.Nodes).FirstOrDefaultAsync(t => t.TreeName == treeName);
            if (tree == null)
            {
                throw new SecureException("Tree did not find.");
            }

            var parentNode = parentNodeId.HasValue
                ? await _context.Nodes.FirstOrDefaultAsync(e => e.Id == parentNodeId.Value)
                : null;

            if (parentNode != null && parentNode.TreeId != tree.Id)
            {
                throw new SecureException("Parent node belongs to a different tree.");
            }

            if (parentNode != null && parentNode.Children.Any(c => c.NodeName == nodeName))
            {
                throw new SecureException("Node name must be unique among siblings.");
            }

            var node = new Node { NodeName = nodeName, ParentNode = parentNode, Tree = tree };
            tree.Nodes.Add(node);
            await _context.SaveChangesAsync();

            return tree;
        }

        public async Task DeleteNodeAsync(
            string treeName,
            Guid nodeId)
        {
            var node = await _context.Nodes.Include(n => n.Children).FirstOrDefaultAsync(n => n.Id == nodeId && n.Tree.TreeName == treeName);

            if (node == null)
            {
                throw new SecureException("Node does not belong to the specified tree.");
            }

            if (node.Children.Any())
            {
                throw new ChildrenNodesDeletionException("You have to delete all children nodes first.");
            }

            _context.Nodes.Remove(node);
            await _context.SaveChangesAsync();
        }

        public async Task RenameNodeAsync(
            string treeName,
            Guid nodeId,
            string newNodeName)
        {
            var node = await _context.Nodes.Include(n => n.ParentNode).FirstOrDefaultAsync(n => n.Id == nodeId && n.Tree.TreeName == treeName);

            if (node == null)
            {
                throw new SecureException("Node does not belong to the specified tree.");
            }

            if (node.ParentNode != null && node.ParentNode.Children.Any(c => c.NodeName == newNodeName))
            {
                throw new SecureException("New node name must be unique among siblings.");
            }

            node.NodeName = newNodeName;
            await _context.SaveChangesAsync();
        }
    }
}
