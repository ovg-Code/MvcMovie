using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class PhoneRepository : Repository<Phone>, IPhoneRepository
{
    public PhoneRepository(ApplicationDbContext context) : base(context)
    {
    }
}
