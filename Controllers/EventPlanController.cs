using McpEventPlanner.Models;
using McpEventPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace McpEventPlanner.Controllers
{
    /// <summary>
    /// בקר לתכנון אירועים
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EventPlanController : ControllerBase
    {
        private readonly EventPlanningOrchestrator _orchestrator;
        private readonly ILogger<EventPlanController> _logger;

        public EventPlanController(EventPlanningOrchestrator orchestrator, ILogger<EventPlanController> logger)
        {
            _orchestrator = orchestrator;
            _logger = logger;
        }

        /// <summary>
        /// יוצר תכנית הפקה שלמה לאירוע
        /// </summary>
        [HttpPost("generate")]
        [EnableRateLimiting("GeneratePlan")]
        public async Task<IActionResult> GenerateEventPlan([FromBody] EventInput eventInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation($"מתחיל תכנון אירוע: {eventInput.EventName} עבור {eventInput.TargetAudience}");
                var plan = await _orchestrator.GenerateEventPlan(eventInput);
                return Ok(plan);
            }
            catch (Exception ex)
            {
                _logger.LogError($"שגיאה בתכנון אירוע: {ex.Message}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// בדיקת בריאות
        /// </summary>
        [HttpGet("health")]
        [EnableRateLimiting("Health")]
        public IActionResult Health()
        {
            return Ok(new { status = "מערכת תכנון אירועים פעילה", timestamp = DateTime.UtcNow });
        }
    }
}
