using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class StateRepository : Repository<State>, IStateRepository
{
    public StateRepository(ApplicationDbContext context) : base(context)
    {
    }
}
