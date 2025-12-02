using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

public class ActorTypeService : IActorTypeService
{
    private readonly IActorTypeRepository _repository;

    public ActorTypeService(IActorTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ActorType>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ActorType?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ActorType> CreateAsync(ActorType entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(ActorType entity)
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
