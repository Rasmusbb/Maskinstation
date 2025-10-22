using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Maskinstation.models;
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

        [HttpPost("Create")]
        public async Task<IActionResult> Create(MachineDTO MachineDTO)
        {
            MachineDTOID Machine = await _context.CreateAsync(MachineDTO);
            if (User == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = Machine.MachineID }, Machine);

        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<MachineDTOID>> GetById(Guid MachineID)
        {
            var Machine = await _context.GetbyIDAsync(MachineID);
            if (Machine == null)
                return NotFound();

            return Ok(Machine);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<MachineDTOID>> GetAll(List<Guid> tags)
        {
            return Ok(await _context.GetAllAsync(tags));
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid MachineID)
        {
            await _context.DeleteAsync(MachineID);
            return Ok();
        }
    }
}
