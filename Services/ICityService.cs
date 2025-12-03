using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para el catalogo de ciudades.
/// </summary>
public interface ICityService
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<City?> GetByIdAsync(Guid id);
    Task<City> CreateAsync(City entity);
    Task UpdateAsync(City entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
