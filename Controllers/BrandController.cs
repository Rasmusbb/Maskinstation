using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Maskinstation.models;
using Microsoft.AspNetCore.Mvc;

namespace Maskinstation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : Controller
    {
        private readonly IBrand _context;

        public BrandController(IBrand BrandService)
        {
            _context = BrandService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(BrandDTO BrandDTO)
        {
            BrandDTOID Brand = await _context.CreateAsync(BrandDTO);
            if (User == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = Brand.BrandID }, Brand);

        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<BrandDTOID>> GetById(Guid BrandID)
        {
            var Brand = await _context.GetByIDAsync(BrandID);
            if (Brand == null)
                return NotFound();

            return Ok(Brand);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<bool>> Update(Guid ID, BrandDTO BrandDTO)
        {
            var Brand = await _context.UpdateAsync(ID, BrandDTO);
            return Ok(Brand);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<BrandDTOID>> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid BrandID)
        {
            await _context.DeleteAsync(BrandID);
            return Ok();
        }
    }
}
