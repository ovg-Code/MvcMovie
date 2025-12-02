using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _repository;

    public CountryService(ICountryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Country?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Country> CreateAsync(Country entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(Country entity)
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
