using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class SocialNetworkRepository : Repository<SocialNetwork>, ISocialNetworkRepository
{
    public SocialNetworkRepository(ApplicationDbContext context) : base(context)
    {
    }
}
