using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class AddressTypeRepository : Repository<AddressType>, IAddressTypeRepository
{
    public AddressTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
