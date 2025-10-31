using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maskinstation.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGallery _context;

        public GalleryController(IGallery GalleryService)
        {
            _context = GalleryService;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(GalleryDTO GalleryDTO)
        {
            GalleryDTOID Gallery = await _context.CreateAsync(GalleryDTO);
            if (Gallery == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = Gallery.GalleryID }, Gallery);

        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<ImageDTOID>> GetById(Guid GalleryID)
        {
            var Gallery = await _context.GetByIDAsync(GalleryID);
            if (Gallery == null)
                return NotFound();

            return Ok(Gallery);
        }

        [HttpPost("AddImageToGallery")]
        public async Task<IActionResult> AddprofilPic(ImageDTOCreation Image)
        {
            return Ok(await _context.AddImageToGallery(Image));

        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<GalleryDTOID>> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }
    }
}
