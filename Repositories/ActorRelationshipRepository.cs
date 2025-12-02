using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class ActorRelationshipRepository : Repository<ActorRelationship>, IActorRelationshipRepository
{
    public ActorRelationshipRepository(ApplicationDbContext context) : base(context)
    {
    }
}
