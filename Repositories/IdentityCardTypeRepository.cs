using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class IdentityCardTypeRepository : Repository<IdentityCardType>, IIdentityCardTypeRepository
{
    public IdentityCardTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
