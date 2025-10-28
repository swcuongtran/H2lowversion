using H2.Application.Interfaces;
using H2.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace H2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreenSensorDataController : ControllerBase
    {
        private readonly IGreenSensorDataRepository _repository;

        public GreenSensorDataController(IGreenSensorDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _repository.GetByIdAsync(id);
            if (data == null) return NotFound();
            return Ok(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetByDevice(
            [FromQuery] string deviceId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return BadRequest("deviceId is required.");

            var data = await _repository.GetByDeviceAsync(deviceId, page, pageSize);
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GreenSensorData model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(model);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }
    }
}
