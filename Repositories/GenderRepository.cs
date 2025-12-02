using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class GenderRepository : Repository<Gender>, IGenderRepository
{
    public GenderRepository(ApplicationDbContext context) : base(context)
    {
    }
}
