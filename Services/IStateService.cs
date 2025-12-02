using ari2._0.Models;

namespace ari2._0.Services;

public interface IStateService
{
    Task<IEnumerable<State>> GetAllAsync();
    Task<State?> GetByIdAsync(Guid id);
    Task<State> CreateAsync(State entity);
    Task UpdateAsync(State entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
