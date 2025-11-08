using Maskinstation.DTOs;

namespace Maskinstation.Interfaces
{
    public interface IBrand : ICRUD<BrandDTO, BrandDTOID>
    {
        Task<BrandDTOID> GetByIDAsync(Guid id);
    }
}
