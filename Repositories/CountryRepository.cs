using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class CountryRepository : Repository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
