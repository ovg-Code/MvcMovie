using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

/// <summary>
/// Implementa el acceso a datos para la entidad Actor.
/// </summary>
public class ActorRepository : Repository<Actor>, IActorRepository
{
    public ActorRepository(ApplicationDbContext context) : base(context)
    {
    }
}
