using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class ActorRelationshipRepository : Repository<ActorRelationship>, IActorRelationshipRepository
{
    public ActorRelationshipRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<ActorRelationship>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.ParentActor)
            .Include(x => x.ChildActor)
            .Include(x => x.RelationshipType)
            .ToListAsync();
    }

    public override async Task<ActorRelationship?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.ParentActor)
            .Include(x => x.ChildActor)
            .Include(x => x.RelationshipType)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}