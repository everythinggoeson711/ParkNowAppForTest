using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parking.FindingSlotManagement.Application.Contracts.Infrastructure;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Parking.FindingSlotManagement.Api.Controllers.Common
{
    /// <summary>
    /// API Controller để upload ảnh lên Cloudinary
    /// </summary>
    [Route("api/upload-image")]
    [ApiController]
    public class ZUploadImageController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ZUploadImageController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Upload ảnh lên Cloudinary
        /// </summary>
        /// <param name="file">File ảnh cần upload</param>
        /// <returns>URL của ảnh sau khi upload</returns>
        [HttpPost]
        public async Task<IActionResult> UploadImagess(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest(new { error = "File is empty" });
                }

                // Upload to Cloudinary
                var imageUrl = await _cloudinaryService.UploadImageAsync(file, "parkz-uploads");

                var response = new Dictionary<string, string>
                {
                    { "link", imageUrl }
                };

                string json = JsonSerializer.Serialize(response);
                return Ok(json);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Upload failed: {ex.Message}" });
            }
        }
    }
}
