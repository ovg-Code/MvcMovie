using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class RelationshipTypeRepository : Repository<RelationshipType>, IRelationshipTypeRepository
{
    public RelationshipTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
