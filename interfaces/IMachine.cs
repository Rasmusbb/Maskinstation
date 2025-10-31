

using Maskinstation.DTOs;
namespace Maskinstation.Interfaces
{
    public interface IMachine : ICRUD<MachineDTO, MachineDTOID>
    {
        Task<IEnumerable<MachineDTOID>> GetByTags(List<Guid> TagIDs);
    }
}
