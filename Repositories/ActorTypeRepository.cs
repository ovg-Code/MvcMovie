using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class ActorTypeRepository : Repository<ActorType>, IActorTypeRepository
{
    public ActorTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
