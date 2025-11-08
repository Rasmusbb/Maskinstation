
using Maskinstation.DTOs;

namespace Maskinstation.Interfaces
{
    public interface ITag : ICRUD<TagDTO, TagDTOID>
    {
        Task<TagDTOID> GetByIDAsync(Guid MachineID);
    }
}
