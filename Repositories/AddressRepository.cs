using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class AddressRepository : Repository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Address>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Actor)
            .Include(x => x.AddressType)
            .Include(x => x.ZipCode)
            .ToListAsync();
    }
}