using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }
}
