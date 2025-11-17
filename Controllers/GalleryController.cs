using Maskinstation.DTOs;
using Maskinstation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Maskinstation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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


        [HttpGet("GetProfilPic")]
        public async Task<IActionResult> GetProfilPic(Guid ImageID)
        {
            var (ms, contentType) = await _context.GetProfilPic(ImageID);
            return File(ms, contentType);
        }


        [HttpGet("GetFirstPicByTag")]
        public async Task<IActionResult> GetFirstPicByTag(Guid GalleryID,string TagName)
        {
            var (ms, contentType) = await _context.GetFirstPicByTag(GalleryID,TagName);
            return File(ms, contentType);
        }

        [HttpPost("AddImageToGallery")]
        [RequestSizeLimit(300_000_000)]
        public async Task<IActionResult> AddprofilPic(ImageDTOCreation Image)
        {
            return Ok(await _context.AddImageToGallery(Image));

        }
        [HttpGet("video/{ImageID}")]
        public async Task<IActionResult> GetVideo(Guid ImageID)
        {
            var (stream, contentType) = await _context.GetVideo(ImageID);
            return File(stream, contentType,enableRangeProcessing: true);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<GalleryDTOID>> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }
    }
}
