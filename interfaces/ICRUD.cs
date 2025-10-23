using Maskinstation.DTOs;

namespace Maskinstation.interfaces
{
    public interface ICRUD<TDto, TDtoId>
    {
        Task<IEnumerable<TDtoId>> GetAllAsync();
        Task<TDtoId> GetByIDAsync(Guid id);
        Task<TDtoId> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(Guid id, TDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
