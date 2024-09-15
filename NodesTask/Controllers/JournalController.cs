namespace NodesTask.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NodesTask.Interfaces;

    [ApiController]
    [Route("api/[controller]")]
    public class JournalController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public JournalController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("getRange")]
        public async Task<IActionResult> GetJournalEntries(int skip = 0, int take = 10, DateTime? from = null, DateTime? to = null, string search = null)
        {
            var entries = await _serviceManager.ExceptionJournalService.GetJournalEntriesAsync(skip, take, from, to, search);
            return Ok(entries);
        }

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
