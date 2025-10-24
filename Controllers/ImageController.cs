using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Maskinstation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maskinstation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class imageController : Controller
    {
        private readonly IImage _context;

        public imageController(IImage imageService)
        {
            _context = imageService;
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ImageDTO imageDTO)
        {
            ImageDTOID image = await _context.CreateAsync(imageDTO);
            if (User == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = image.ImageID }, image);

        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<ImageDTOID>> GetById(Guid imageID)
        {
            var image = await _context.GetByIDAsync(imageID);
            if (image == null)
                return NotFound();

            return Ok(image);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Edit")]
        public async Task<ActionResult<bool>> Update(Guid ID, ImageDTO imageDTO)
        {
            var image = await _context.UpdateAsync(ID, imageDTO);
            return Ok(image);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<ImageDTOID>> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }

        [Authorize]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid imageID)
        {
            await _context.DeleteAsync(imageID);
            return Ok();
        }
    }
}
