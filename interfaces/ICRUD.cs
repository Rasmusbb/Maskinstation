using Maskinstation.DTOs;

namespace Maskinstation.Interfaces
{
    public interface ICRUD<TDto, TDtoId>
    {
        Task<IEnumerable<TDtoId>> GetAllAsync();
        Task<TDtoId> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(Guid id, TDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
