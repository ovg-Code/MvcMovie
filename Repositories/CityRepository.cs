using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(ApplicationDbContext context) : base(context)
    {
    }
}
