using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class SocialNetworkRepository : Repository<SocialNetwork>, ISocialNetworkRepository
{
    public SocialNetworkRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<SocialNetwork>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Actor)
            .ToListAsync();
    }
}