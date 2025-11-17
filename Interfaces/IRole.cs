
using Maskinstation.DTOs;

namespace Maskinstation.Interfaces
{
    public interface IRole : ICRUD<RoleDTO, RoleDTOID>
    {
        Task<RoleDTOID> GetByIDAsync(Guid RoleID);
    }
}
