using ari2._0.Models;

namespace ari2._0.Services;

public interface ICustomerPublicStatusTypeService
{
    Task<IEnumerable<CustomerPublicStatusType>> GetAllAsync();
    Task<CustomerPublicStatusType?> GetByIdAsync(Guid id);
    Task<CustomerPublicStatusType> CreateAsync(CustomerPublicStatusType entity);
    Task UpdateAsync(CustomerPublicStatusType entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
