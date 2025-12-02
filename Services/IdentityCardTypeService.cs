using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

public class IdentityCardTypeService : IIdentityCardTypeService
{
    private readonly IIdentityCardTypeRepository _repository;

    public IdentityCardTypeService(IIdentityCardTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<IdentityCardType>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IdentityCardType?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IdentityCardType> CreateAsync(IdentityCardType entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(IdentityCardType entity)
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
