using Mapster;
using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.Models;
using Maskinstation.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Maskinstation.Services
{
    public class RoleService : IRole
    {
        private readonly MaskinstationContext _context;

        public RoleService(MaskinstationContext context)
        {
            _context = context;
        }

        string DBNullText = "Database context is not available.";
        public async Task<IEnumerable<RoleDTOID>> GetAllAsync()
        {
            if (_context.Roles == null)
            {
                throw new InvalidOperationException(DBNullText);
            }

            List<Role> roles = await _context.Roles.ToListAsync();
            IEnumerable<RoleDTOID> rolesDTO = roles.Adapt<IEnumerable<RoleDTOID>>();
            return rolesDTO;

        }
        public async Task<RoleDTOID> GetByIDAsync(Guid RoleID)
        {
            if (_context.Roles == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Role role = await _context.Roles.FirstOrDefaultAsync();
            return role.Adapt<RoleDTOID>();
        }

        public async Task<RoleDTOID> CreateAsync(RoleDTO RoleDTO)
        {
            if (_context.Roles == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Role Role = RoleDTO.Adapt<Role>();
            _context.Roles.Add(Role);
            await _context.SaveChangesAsync();
            return Role.Adapt<RoleDTOID>();
        }
        public async Task<bool> UpdateAsync(Guid RoleID, RoleDTO Role)
        {
            if (_context.Brands == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid RoleID)
        {

            if (_context.Roles == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Role Role = await _context.Roles.FindAsync(RoleID);
            _context.Roles.Remove(Role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
