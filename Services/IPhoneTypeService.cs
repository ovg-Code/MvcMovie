using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para el catalogo de tipos de telefono.
/// </summary>
public interface IPhoneTypeService
{
    Task<IEnumerable<PhoneType>> GetAllAsync();
    Task<PhoneType?> GetByIdAsync(Guid id);
    Task<PhoneType> CreateAsync(PhoneType entity);
    Task UpdateAsync(PhoneType entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
