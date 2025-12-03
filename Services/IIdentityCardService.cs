using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para la gestion de documentos de identidad.
/// </summary>
public interface IIdentityCardService
{
    Task<IEnumerable<IdentityCard>> GetAllAsync();
    Task<IdentityCard?> GetByIdAsync(Guid id);
    Task<IdentityCard> CreateAsync(IdentityCard entity);
    Task UpdateAsync(IdentityCard entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
