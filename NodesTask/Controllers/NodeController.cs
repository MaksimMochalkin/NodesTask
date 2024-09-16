namespace NodesTask.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NodesTask.Interfaces;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents tree node API
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class NodeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public NodeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Create a new node in your tree.
        /// You must to specify a parent node ID that belongs to your tree. A new node name must be unique across all siblings.
        /// </summary>
        /// <param name="treeName">The name of the tree</param>
        /// <param name="parentNodeId">The parent node id</param>
        /// <param name="nodeName">The name of the node</param>
        /// <returns></returns>
        [HttpPost("createNode")]
        public async Task<IActionResult> CreateNode(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] Guid? parentNodeId,
            [FromQuery][Required] string nodeName)
        {
            var tree = await _serviceManager.NodeService.CreateNodeAsync(treeName, parentNodeId, nodeName);
            return Ok(tree);
        }

        /// <summary>
        /// Delete an existing node in your tree. You must specify a node ID that belongs your tree.
        /// </summary>
        /// <param name="treeName">The name of the tree</param>
        /// <param name="nodeId">The id of the node</param>
        /// <returns></returns>
        [HttpPost("deleteNode")]
        public async Task<IActionResult> DeleteNode(
            [FromQuery][Required] string treeName,
            [FromQuery][Required] Guid nodeId)
        {
            await _serviceManager.NodeService.DeleteNodeAsync(treeName, nodeId);
            return NoContent();
        }

        /// <summary>
        /// Rename an existing node in your tree. You must specify a node ID that belongs your tree.
        /// A new name of the node must be unique across all siblings.
        /// </summary>
        /// <param name="treeName">The name of the tree</param>
        /// <param name="nodeId">The id of the node</param>
        /// <param name="newNodeName">The new name of the node</param>
        /// <returns>The entire tree structure</returns>
        [HttpPost("renameNode")]
        public async Task<IActionResult> RenameNode(
            [FromQuery][Required] string treeName, 
            [FromQuery][Required] Guid nodeId,
            [FromQuery][Required] string newNodeName)
        {
            await _serviceManager.NodeService.RenameNodeAsync(treeName, nodeId, newNodeName);
            return NoContent();
        }
    }
}
