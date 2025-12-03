using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para el catalogo de codigos postales.
/// </summary>
public interface IZipCodeService
{
    Task<IEnumerable<ZipCode>> GetAllAsync();
    Task<ZipCode?> GetByIdAsync(Guid id);
    Task<ZipCode> CreateAsync(ZipCode entity);
    Task UpdateAsync(ZipCode entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
