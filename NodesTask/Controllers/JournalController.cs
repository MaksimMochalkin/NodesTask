namespace NodesTask.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NodesTask.Interfaces;
    using NodesTask.Models.Requests;

    /// <summary>
    /// Represents journal API
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JournalController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public JournalController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Provides the pagination API. Skip means the number of items should be skipped by server.
        /// Take means the maximum number items should be returned by server.
        /// All fields of the filter are optional.
        /// Example 'search' command format: "where:QueryParams:abc, equal:ExceptionType:System.Exception, notequal:EventId:Guid"
        /// </summary>
        /// <param name="skip">The name of the tree</param>
        /// <param name="take">The parent node id</param>
        /// <returns></returns>
        [HttpPost("getRange")]
        public async Task<IActionResult> GetJournalEntries(
            [FromQuery] int skip,
            [FromQuery] int take, 
            [FromBody] FilterParams filter)
        {
            var entries = await _serviceManager.ExceptionJournalService.GetJournalEntriesAsync(skip, take, filter);
            return Ok(entries);
        }

        /// <summary>
        /// Returns the information about an particular event by ID.
        /// </summary>
        /// <param name="id">The id of the event</param>
        /// <returns></returns>
        [HttpGet("getSingle")]
        public async Task<IActionResult> GetJournalEntryById(Guid id)
        {
            var entry = await _serviceManager.ExceptionJournalService.GetJournalEntryByIdAsync(id);

            if (entry == null)
                return NotFound();

            return Ok(entry);
        }
    }
}
