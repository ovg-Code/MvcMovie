using ari2._0.Models;

namespace ari2._0.Repositories;

/// <summary>
/// Define operaciones de acceso a datos especificas para clientes.
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    Task<Customer?> GetCustomerWithDetailsAsync(Guid id);
}
