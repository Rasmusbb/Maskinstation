
using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.interfaces;
using Maskinstation.models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

        public async Task<IEnumerable<MachineDTOID>> GetByTags(List<Guid> TagIDs)
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
            List<Tag> tags = await _context.Tags.Where(t => TagIDs.Contains(t.TagID)).ToListAsync();
            if (tags.Count != 0)
            {
                //Machines = _context.Machines.Where(m => m.MachineTags.Any(t => tags.Contains(t))).Include(m => m.images).Include(m => m.Brand).Adapt<IEnumerable<MachineDTOID>>();
            }
            else
            {
                Machines = _context.Machines.Include(m => m.images).Include(m => m.Brand).Adapt<IEnumerable<MachineDTOID>>();
            }
            return Machines;
        }

        public async Task<IEnumerable<MachineDTOID>> GetAllAsync()
        {
            IEnumerable<MachineDTOID> Machines = new List<MachineDTOID>();
            if (_context.Machines == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Machines = _context.Machines.Include(m => m.images).Include(m => m.Brand).Adapt<IEnumerable<MachineDTOID>>();
           
            return Machines;

        }

        public async Task<MachineDTOID> GetByIDAsync(Guid MachineID)
        {
            if(_context.Machines == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Machine machine = await _context.Machines.FindAsync(MachineID);
            return machine.Adapt<MachineDTOID>();
        }

        public async Task<bool> UpdateAsync(Guid MachineID, MachineDTO Machine) 
        {
            if(_context.Machines != null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid MachineID)
        {
            throw new NotImplementedException();
        }
    }
}
