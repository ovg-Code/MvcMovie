using ari2._0.Models;

namespace ari2._0.Services;

public interface ISocialNetworkService
{
    Task<IEnumerable<SocialNetwork>> GetAllAsync();
    Task<SocialNetwork?> GetByIdAsync(Guid id);
    Task<SocialNetwork> CreateAsync(SocialNetwork entity);
    Task UpdateAsync(SocialNetwork entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
