
using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Maskinstation.models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Maskinstation.Services
{
    public class MachineService : IMachine
    {
        private readonly MaskinstationContext _context;

        public MachineService(MaskinstationContext context)
        {
            _context = context;
        }

        string DBNullText = "Database context is not available.";
        public async Task<MachineDTOID> CreateAsync(MachineDTO MachineDTO)
        {
            if (_context.Machines == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Machine machine = MachineDTO.Adapt<Machine>();
            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();
            return machine.Adapt<MachineDTOID>();
        }

        public async Task<IEnumerable<MachineDTOID>> GetAllAsync(List<Guid> tagIDs)
        {
            IEnumerable<MachineDTOID> Machines = new List<MachineDTOID>();
            if (_context.Machines == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            if (_context.Tags == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            List<Tag> tags = await _context.Tags.Where(t => tagIDs.Contains(t.TagID)).ToListAsync();
            if (tags.Count != 0)
            {
                Machines = _context.Machines.Where(m => m.Tags.Any(t => tags.Contains(t))).Include(m => m.images).Include(m => m.Brand).Adapt<IEnumerable<MachineDTOID>>();
            }
            else
            {
                Machines = _context.Machines.Include(m => m.images).Include(m => m.Brand).Adapt<IEnumerable<MachineDTOID>>();
            }
            return Machines;

        }
        public async Task<MachineDTOID> GetbyIDAsync(Guid MachineID)
        {
            if(_context.Machines == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Machine machine = await _context.Machines.FindAsync(MachineID);
            return machine.Adapt<MachineDTOID>();
        }

        public async Task<bool> UpdateAsync(MachineDTO Machine)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid MachineID)
        {
            throw new NotImplementedException();
        }
    }
}
