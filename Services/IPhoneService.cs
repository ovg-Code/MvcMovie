using ari2._0.Models;

namespace ari2._0.Services;

/// <summary>
/// Define las operaciones de negocio para la gestion de telefonos.
/// </summary>
public interface IPhoneService
{
    Task<IEnumerable<Phone>> GetAllAsync();
    Task<Phone?> GetByIdAsync(Guid id);
    Task<Phone> CreateAsync(Phone entity);
    Task UpdateAsync(Phone entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
