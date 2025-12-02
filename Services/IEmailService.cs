using ari2._0.Models;

namespace ari2._0.Services;

public interface IEmailService
{
    Task<IEnumerable<Email>> GetAllAsync();
    Task<Email?> GetByIdAsync(Guid id);
    Task<Email> CreateAsync(Email entity);
    Task UpdateAsync(Email entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
