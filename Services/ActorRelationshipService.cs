using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

public class ActorRelationshipService : IActorRelationshipService
{
    private readonly IActorRelationshipRepository _repository;

    public ActorRelationshipService(IActorRelationshipRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ActorRelationship>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ActorRelationship?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ActorRelationship> CreateAsync(ActorRelationship entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(ActorRelationship entity)
    {
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _repository.ExistsAsync(id);
    }
}
