using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class PhoneRepository : Repository<Phone>, IPhoneRepository
{
    public PhoneRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Phone>> GetAllAsync()
    {
        return await _dbSet
            .Include(p => p.Actor)
            .Include(p => p.PhoneType)
            .ToListAsync();
    }

    public override async Task<Phone?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Actor)
            .Include(p => p.PhoneType)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
