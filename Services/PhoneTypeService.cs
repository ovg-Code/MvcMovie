using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

public class PhoneTypeService : IPhoneTypeService
{
    private readonly IPhoneTypeRepository _repository;

    public PhoneTypeService(IPhoneTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PhoneType>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<PhoneType?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<PhoneType> CreateAsync(PhoneType entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(PhoneType entity)
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
