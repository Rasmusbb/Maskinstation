using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Maskinstation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maskinstation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineController : Controller
    {
        private readonly IMachine _context;

        public MachineController(IMachine MachineService)
        {
            _context = MachineService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(MachineDTO MachineDTO)
        {
            MachineDTOID Machine = await _context.CreateAsync(MachineDTO);
            if (Machine == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = Machine.MachineID }, Machine);

        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<MachineDTOID>> GetById(Guid MachineID)
        {
            var Machine = await _context.GetByIDAsync(MachineID);
            if (Machine == null)
                return NotFound();

            return Ok(Machine);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Edit")]
        public async Task<ActionResult<bool>> Update(Guid ID, MachineDTO machine)
        {
            var Machine = await _context.UpdateAsync(ID,machine);
            return Ok(machine);
        }

        [HttpGet("GetByTags")]
        public  async Task<ActionResult<MachineDTOID>> GetByTags(List<Guid> TagIDs)
        {
            return Ok(await _context.GetByTags(TagIDs));
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<MachineDTOID>> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid MachineID)
        {
            await _context.DeleteAsync(MachineID);
            return Ok();
        }
    }
}
