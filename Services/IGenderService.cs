using ari2._0.Models;

namespace ari2._0.Services;

public interface IGenderService
{
    Task<IEnumerable<Gender>> GetAllAsync();
    Task<Gender?> GetByIdAsync(Guid id);
    Task<Gender> CreateAsync(Gender entity);
    Task UpdateAsync(Gender entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
