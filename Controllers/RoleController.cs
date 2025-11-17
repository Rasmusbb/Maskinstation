using Maskinstation.DTOs;
using Maskinstation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maskinstation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRole _context;

        public RoleController(IRole RoleService)
        {
            _context = RoleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(RoleDTO RoleDTO)
        {
            RoleDTOID Role = await _context.CreateAsync(RoleDTO);
            if (User == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = Role.RoleID }, Role);

        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<RoleDTOID>> GetById(Guid RoleID)
        {
            var Role = await _context.GetByIDAsync(RoleID);
            if (Role == null)
                return NotFound();

            return Ok(Role);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Edit")]
        public async Task<ActionResult<bool>> Update(Guid ID, RoleDTO RoleDTO)
        {
            var Role = await _context.UpdateAsync(ID, RoleDTO);
            return Ok(Role);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<RoleDTOID>> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid RoleID)
        {
            await _context.DeleteAsync(RoleID);
            return Ok();
        }
    }
}
