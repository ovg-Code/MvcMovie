using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class IdentityCardRepository : Repository<IdentityCard>, IIdentityCardRepository
{
    public IdentityCardRepository(ApplicationDbContext context) : base(context)
    {
    }
}
