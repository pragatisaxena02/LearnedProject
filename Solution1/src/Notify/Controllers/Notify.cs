using Microsoft.AspNetCore.Mvc;
using Notify.Services;

namespace Notify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Notify : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<Notify> _logger;
        private readonly INotify _notify;

        public Notify(ILogger<Notify> logger, INotify notify)
        {
            _logger = logger;
            _notify = notify;
        }

        [HttpGet]
        public async Task<ActionResult<bool>> Get()
        {
            await _notify.SendNotification().ConfigureAwait(false);
            return true;
        }
    }
}
