using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

public class IdentityCardService : IIdentityCardService
{
    private readonly IIdentityCardRepository _repository;

    public IdentityCardService(IIdentityCardRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<IdentityCard>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IdentityCard?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IdentityCard> CreateAsync(IdentityCard entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(IdentityCard entity)
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
