using ari2._0.Models;

namespace ari2._0.Services;

public interface IIdentityCardTypeService
{
    Task<IEnumerable<IdentityCardType>> GetAllAsync();
    Task<IdentityCardType?> GetByIdAsync(Guid id);
    Task<IdentityCardType> CreateAsync(IdentityCardType entity);
    Task UpdateAsync(IdentityCardType entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
