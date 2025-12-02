using ari2._0.Models;

namespace ari2._0.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    Task<Customer?> GetCustomerWithDetailsAsync(Guid id);
}
