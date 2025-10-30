using H2.Application.DTOs;
using H2.Application.Interfaces;
using H2.Application.Usecases;
using H2.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace H2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreenImageController : ControllerBase
    {
        private readonly IGreenImageRepository _repository;
        private readonly UploadImageHandler _uploadImageHandler;


        public GreenImageController(IGreenImageRepository repository, UploadImageHandler uploadImageHandler)
        {
            _repository = repository;
            _uploadImageHandler = uploadImageHandler;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null) return NotFound();
            return Ok(image);
        }

        [HttpGet]
        public async Task<IActionResult> GetByDevice(
            [FromQuery] string deviceId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return BadRequest("deviceId is required.");

            var images = await _repository.GetByDeviceAsync(deviceId, page, pageSize);
            return Ok(images);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(
            [FromForm] IFormFile formFile,
            [FromForm] string deviceId)
        {
            if (formFile == null || formFile.Length == 0)
                return BadRequest("No file uploaded.");

            if (string.IsNullOrWhiteSpace(deviceId))
                return BadRequest("deviceId is required.");

            var dto = new UploadImageDTO
            {
                Stream = formFile.OpenReadStream(),
                FileName = formFile.FileName,
                ContentType = formFile.ContentType ?? "image/jpeg",
                DeviceId = deviceId,
            };

            // Upload to storage → trả về URL
            var imageUrl = await _uploadImageHandler.HandleAsync(dto, CancellationToken.None);

            // Save metadata to database
            var entity = new GreenImage
            {
                Id = Guid.NewGuid(),
                DeviceId = deviceId,
                ImageUrl = imageUrl,
                Timestamp = DateTime.UtcNow
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestByDevice([FromQuery] string deviceId)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return BadRequest("deviceId is required.");

            var latest = await _repository.GetLatestByDeviceAsync(deviceId);
            if (latest == null)
                return NotFound($"No images found for deviceId = {deviceId}");

            return Ok(new
            {
                deviceId = deviceId,
                imageUrl = latest.ImageUrl,
                timestamp = latest.Timestamp
            });
        }

    }
}
