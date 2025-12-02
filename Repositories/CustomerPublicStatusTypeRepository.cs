using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class CustomerPublicStatusTypeRepository : Repository<CustomerPublicStatusType>, ICustomerPublicStatusTypeRepository
{
    public CustomerPublicStatusTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
