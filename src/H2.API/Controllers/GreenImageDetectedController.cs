using H2.Application.Interfaces;
using H2.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreenImageDetectedController : ControllerBase
    {
        private readonly IGreenImageDetectedRepository _repository;

        public GreenImageDetectedController(IGreenImageDetectedRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var detected = await _repository.GetByIdAsync(id);
            if (detected == null)
                return NotFound("Detected image not found.");

            return Ok(detected);
        }

        [HttpGet]
        public async Task<IActionResult> GetByDevice(
            [FromQuery] string deviceId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return BadRequest("deviceId is required.");

            var list = await _repository.GetByDeviceAsync(deviceId, page, pageSize);

            return Ok(new
            {
                total = list.Count(),
                page,
                pageSize,
                data = list
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GreenImageDetected detected)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            detected.Id = Guid.NewGuid();
            detected.ProcessedAt = DateTime.UtcNow;

            await _repository.AddAsync(detected);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = detected.Id }, detected);
        }
    }
}
