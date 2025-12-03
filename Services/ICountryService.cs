using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para el catalogo de paises.
/// </summary>
public interface ICountryService
{
    Task<IEnumerable<Country>> GetAllAsync();
    Task<Country?> GetByIdAsync(Guid id);
    Task<Country> CreateAsync(Country entity);
    Task UpdateAsync(Country entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
