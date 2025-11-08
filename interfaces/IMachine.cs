

using Maskinstation.DTOs;
namespace Maskinstation.Interfaces
{
    public interface IMachine : ICRUD<MachineDTO, MachineDTOID>
    {
        Task<IEnumerable<MachineDTOID>> GetByTags(List<Guid> TagIDs);
        Task<MachineDTOID> AddTag(Guid MachineID, Guid TagID);
        Task<MachineDTOID> AddDriver(Guid MachineID, Guid UserID);
        Task<MachineDTOID> GetByIDAsync(Guid MachineID);
    }
}
