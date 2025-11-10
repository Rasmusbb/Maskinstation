using Maskinstation.DTOs;
using Maskinstation.Interfaces;
using Maskinstation.Models;
using Maskinstation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

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

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTOIDTokens>> Login(UserLoingObject UserLogin)
        {
            UserTokens Tokens = await _context.Login(UserLogin);
            if (Tokens == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(Tokens);
            }
        }

        [Authorize]
        [HttpPatch("ChangePassword")]
        public async Task<ActionResult<string>> ChangePassword(Guid UserID,string NewPassword)
        {
            try
            {
                return await _context.ChangePassword(UserID, NewPassword);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User Not Found");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserDTOPas UserDTO)
        {
            try
            {
                UserDTOID User = await _context.CreateAsync(UserDTO);
                if (User == null)
                {
                    return BadRequest();
                }
                return CreatedAtAction(nameof(GetById), new { id = User.UserID }, User);
            }
            catch(DbUpdateException dbex)
            {
                return Conflict($"An error occurred while saving the entity changes {dbex}");
            }

            

        }





        [HttpPost("RefreshToken")]
        public async Task<IActionResult> LoginByRefreshToken(RefreshTokenUser Token)
        {
            try
            {
                UserTokens Tokens = await _context.RefreshToken(Token);
                if (Tokens == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(Tokens);
                }
            }catch(KeyNotFoundException Kex)
            {
                return StatusCode(404, $"UserID didn't match any in the stystem {Kex}");
            }
        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<UserDTOID>> GetById(Guid UserID)
        {
            var User = await _context.GetByIDAsync(UserID);
            if (User == null)
                return NotFound();

            return Ok(User);
        }

        [Authorize]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(Guid UserID)
        {
            await _context.DeleteAsync(UserID);
            return Ok();
        }
    }
}
