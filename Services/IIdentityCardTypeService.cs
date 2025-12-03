using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para el catalogo de tipos de documento.
/// </summary>
public interface IIdentityCardTypeService
{
    Task<IEnumerable<IdentityCardType>> GetAllAsync();
    Task<IdentityCardType?> GetByIdAsync(Guid id);
    Task<IdentityCardType> CreateAsync(IdentityCardType entity);
    Task UpdateAsync(IdentityCardType entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
