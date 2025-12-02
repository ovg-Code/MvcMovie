using ari2._0.Models;
using ari2._0.Repositories;

namespace ari2._0.Services;

public class EmailService : IEmailService
{
    private readonly IEmailRepository _repository;

    public EmailService(IEmailRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Email>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Email?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Email> CreateAsync(Email entity)
    {
        return await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(Email entity)
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
