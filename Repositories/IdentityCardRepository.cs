using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class IdentityCardRepository : Repository<IdentityCard>, IIdentityCardRepository
{
    public IdentityCardRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<IdentityCard>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Actor)
            .Include(x => x.IdentityCardType)
            .ToListAsync();
    }
}