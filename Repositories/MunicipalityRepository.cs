using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class MunicipalityRepository : Repository<Municipality>, IMunicipalityRepository
{
    public MunicipalityRepository(ApplicationDbContext context) : base(context)
    {
    }
}
