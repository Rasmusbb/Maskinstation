

using Maskinstation.DTOs;
using Maskinstation.models;
namespace Maskinstation.interfaces
{
    public interface IMachine
    {
        Task<IEnumerable<MachineDTOID>> GetAllAsync(List<Guid> tags);
        Task<MachineDTOID> GetbyIDAsync(Guid MachineID);
        Task<MachineDTOID> CreateAsync(MachineDTO Machine);
        Task<bool> UpdateAsync(MachineDTO Machine);
        Task<bool> DeleteAsync(Guid MachineID);
    }
}
