namespace NodesTask.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NodesTask.Data.Entities;
    using NodesTask.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public TreeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Represents entire tree API.
        /// Returns the entire tree. If the tree doesn't exist, it will be created automatically.
        /// </summary>
        /// <param name="treeName">The name of the tree</param>
        /// <returns>The entire tree structure</returns>
        [HttpPost("getTree")]
        public async Task<IActionResult> GetTree([FromQuery] string treeName)
        {
            if (string.IsNullOrWhiteSpace(treeName))
            {
                return BadRequest("Tree name is required.");
            }

            var tree = await _serviceManager.TreeService.GetTreeByNameAsync(treeName);

            if (tree == null)
            {
                tree = new Tree
                {
                    TreeName = treeName,
                    Nodes = new List<Node>()
                };

                await _serviceManager.TreeService.CreateTreeAsync(tree);
            }

            var entireTree = await _serviceManager.TreeService.GetTreeWithNodesAsync(treeName);
            return Ok(entireTree);
        }
    }
}
