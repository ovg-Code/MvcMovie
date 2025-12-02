using ari2._0.Data;
using ari2._0.Models;

namespace ari2._0.Repositories;

public class EmailRepository : Repository<Email>, IEmailRepository
{
    public EmailRepository(ApplicationDbContext context) : base(context)
    {
    }
}
