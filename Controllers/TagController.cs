using Maskinstation.DTOs;
using Maskinstation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maskinstation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly ITag _context;

        public TagController(ITag TagService)
        {
            _context = TagService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(TagDTO TagDTO)
        {
            TagDTOID Tag = await _context.CreateAsync(TagDTO);
            if (User == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = Tag.TagID }, Tag);

        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<TagDTOID>> GetById(Guid TagID)
        {
            var Tag = await _context.GetByIDAsync(TagID);
            if (Tag == null)
                return NotFound();

            return Ok(Tag);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Edit")]
        public async Task<ActionResult<bool>> Update(Guid ID, TagDTO TagDTO)
        {
            var Tag = await _context.UpdateAsync(ID, TagDTO);
            return Ok(Tag);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<TagDTOID>> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid TagID)
        {
            await _context.DeleteAsync(TagID);
            return Ok();
        }
    }
}
