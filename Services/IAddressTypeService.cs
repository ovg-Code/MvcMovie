using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para el catalogo de tipos de direccion.
/// </summary>
public interface IAddressTypeService
{
    Task<IEnumerable<AddressType>> GetAllAsync();
    Task<AddressType?> GetByIdAsync(Guid id);
    Task<AddressType> CreateAsync(AddressType entity);
    Task UpdateAsync(AddressType entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
