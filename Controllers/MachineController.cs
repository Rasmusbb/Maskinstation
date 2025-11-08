using Maskinstation.DTOs;
using Maskinstation.Interfaces;
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

        [Authorize(Roles = "Admin")]
        [HttpPut("AddTag")]
        public async Task<ActionResult<bool>> AddTag(Guid MachineID,Guid TagID)
        {
            MachineDTOID Machine = await _context.AddTag(MachineID, TagID);
            return Ok(Machine);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("AddDriver")]
        public async Task<ActionResult<bool>> AddDriver(Guid MachineID, Guid UserID)
        {
            MachineDTOID Machine = await _context.AddDriver(MachineID, UserID);
            return Ok(Machine);
        }

        [HttpGet("GetByTags")]
        public async Task<ActionResult<MachineDTOID>> GetByTags([FromQuery] List<Guid> TagIDs)
        {
            var result = await _context.GetByTags(TagIDs);
            return Ok(result);
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
