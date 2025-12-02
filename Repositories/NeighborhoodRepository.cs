using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class NeighborhoodRepository : Repository<Neighborhood>, INeighborhoodRepository
{
    public NeighborhoodRepository(ApplicationDbContext context) : base(context)
    {
    }
}
