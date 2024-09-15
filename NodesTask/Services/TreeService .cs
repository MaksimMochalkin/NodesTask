namespace NodesTask.Services
{
    using Microsoft.EntityFrameworkCore;
    using NodesTask.Data;
    using NodesTask.Data.Entities;
    using NodesTask.Interfaces;

    public class TreeService : ITreeService
    {
        private readonly NodesApplicationDbContext _context;

        public TreeService(NodesApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Tree> GetTreeByNameAsync(string treeName)
        {
            return await _context.Trees
                .FirstOrDefaultAsync(t => t.TreeName == treeName);
        }

        public async Task<Tree> GetTreeWithNodesAsync(string treeName)
        {
            return await _context.Trees
                .Include(t => t.Nodes)
                .FirstOrDefaultAsync(t => t.TreeName == treeName);
        }

        public async Task CreateTreeAsync(Tree tree)
        {
            await _context.Trees.AddAsync(tree);
            await _context.SaveChangesAsync();
        }
    }
}
