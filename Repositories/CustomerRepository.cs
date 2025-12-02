using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
    {
        return await _dbSet
            .Where(c => c.IsEnabled == true)
            .ToListAsync();
    }

    public async Task<Customer?> GetCustomerWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
