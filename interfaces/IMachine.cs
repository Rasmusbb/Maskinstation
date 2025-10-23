

using Maskinstation.DTOs;
using Maskinstation.models;
namespace Maskinstation.interfaces
{
    public interface IMachine : ICRUD<MachineDTO, MachineDTOID>
    {
        Task<IEnumerable<MachineDTOID>> GetByTags(List<Guid> TagIDs);
    }
}
