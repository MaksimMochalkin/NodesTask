namespace NodesTask.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NodesTask.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    public class NodeController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public NodeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("createNode")]
        public async Task<IActionResult> CreateNode(string treeName, Guid? parentNodeId, string nodeName)
        {
            var tree = await _serviceManager.NodeService.CreateNodeAsync(treeName, parentNodeId, nodeName);
            return Ok(tree);
        }

        [HttpPost("deleteNode")]
        public async Task<IActionResult> DeleteNode(string treeName, Guid nodeId)
        {
            await _serviceManager.NodeService.DeleteNodeAsync(treeName, nodeId);
            return NoContent();
        }

        [HttpPost("renameNode")]
        public async Task<IActionResult> RenameNode(string treeName, Guid nodeId, string newNodeName)
        {
            await _serviceManager.NodeService.RenameNodeAsync(treeName, nodeId, newNodeName);
            return NoContent();
        }
    }
}
