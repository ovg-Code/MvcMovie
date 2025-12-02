using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class ZipCodeRepository : Repository<ZipCode>, IZipCodeRepository
{
    public ZipCodeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
