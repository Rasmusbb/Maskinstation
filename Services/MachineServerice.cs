
using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.Interfaces;
using Maskinstation.Models;
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
            machine.Gallery = new Gallery
            {
                Name = machine.MachineID + "'s Gallery"
            };
            await _context.SaveChangesAsync();
            return machine.Adapt<MachineDTOID>();
        }


        public async Task<MachineDTOID> AddTag(Guid MachineID, Guid TagID)
        {
            Machine machine = await _context.Machines.FindAsync(MachineID);
            if (machine == null)
            {
                throw new KeyNotFoundException($"Machine with ID '{MachineID}' was not found.");
            }
            Tag tag = await _context.Tags.FindAsync(TagID);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with ID '{TagID}' was not found.");
            }
            machine.Tags.Add(tag);
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
                Machines = _context.Machines.Include(m => m.Gallery).Include(m => m.Brand).Adapt<IEnumerable<MachineDTOID>>();
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
            Machines = _context.Machines.Include(m => m.Gallery).Include(m => m.Brand).Adapt<IEnumerable<MachineDTOID>>();
           
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
