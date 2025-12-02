using ari2._0.Models;

namespace ari2._0.Services;

public interface IMunicipalityService
{
    Task<IEnumerable<Municipality>> GetAllAsync();
    Task<Municipality?> GetByIdAsync(Guid id);
    Task<Municipality> CreateAsync(Municipality entity);
    Task UpdateAsync(Municipality entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
