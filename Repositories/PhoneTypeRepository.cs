using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class PhoneTypeRepository : Repository<PhoneType>, IPhoneTypeRepository
{
    public PhoneTypeRepository(ApplicationDbContext context) : base(context)
    {
    }
}
