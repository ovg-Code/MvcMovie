using ari2._0.Models;

namespace ari2._0.Services;

public interface IActorService
{
    Task<IEnumerable<Actor>> GetAllAsync();
    Task<Actor?> GetByIdAsync(Guid id);
    Task<Actor> CreateAsync(Actor entity);
    Task UpdateAsync(Actor entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
