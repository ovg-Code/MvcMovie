using ari2._0.Models;

namespace ari2._0.Services;

public interface IAddressService
{
    Task<IEnumerable<Address>> GetAllAsync();
    Task<Address?> GetByIdAsync(Guid id);
    Task<Address> CreateAsync(Address entity);
    Task UpdateAsync(Address entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
