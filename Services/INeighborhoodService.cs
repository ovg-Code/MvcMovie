using ari2._0.Models;

namespace ari2._0.Services;

public interface INeighborhoodService
{
    Task<IEnumerable<Neighborhood>> GetAllAsync();
    Task<Neighborhood?> GetByIdAsync(Guid id);
    Task<Neighborhood> CreateAsync(Neighborhood entity);
    Task UpdateAsync(Neighborhood entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
