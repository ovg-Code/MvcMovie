using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class EmailRepository : Repository<Email>, IEmailRepository
{
    public EmailRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Email>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Actor)
            .ToListAsync();
    }

    public override async Task<Email?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.Actor)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}