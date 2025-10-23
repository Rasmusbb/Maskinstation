using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Maskinstation.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Maskinstation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUser _context;

        public UserController(IUser UserService)
        {
            _context = UserService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserDTOPas UserDTO)
        {
            UserDTOID User = await _context.CreateAsync(UserDTO);
            if (User == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetById), new { id = User.UserID }, User);

        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<UserDTOID>> GetById(Guid UserID)
        {
            var User = await _context.GetByIDAsync(UserID);
            if (User == null)
                return NotFound();

            return Ok(User);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<bool>> Update(Guid ID, UserDTO UserDTO)
        {
            var User = await _context.UpdateAsync(ID, UserDTO);
            return Ok(User);
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<UserDTOID>> GetAll()
        {
            return Ok(await _context.GetAllAsync());
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid UserID)
        {
            await _context.DeleteAsync(UserID);
            return Ok();
        }
    }
}
