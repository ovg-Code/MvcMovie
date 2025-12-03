using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

/// <summary>
/// Implementa la logica de negocio para la gestion de actores.
/// </summary>
public class ActorService : IActorService
{
    private readonly IActorRepository _repository;

    public ActorService(IActorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Actor?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Actor> CreateAsync(Actor entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(Actor entity)
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
