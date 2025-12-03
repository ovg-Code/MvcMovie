using ari2._0.Data;
using ari2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace ari2._0.Repositories;

/// <summary>
/// Implementa el acceso a datos para la entidad Actor.
/// </summary>
public class ActorRepository : Repository<Actor>, IActorRepository
{
    public ActorRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<dynamic>> GetAllWithRelationsAsync()
    {
        var result = await _context.Actors
            .Select(a => new
            {
                a.Id,
                a.FirstFirstName,
                a.SecondFirstName,
                a.LastFirstName,
                a.LastSecondName,
                ActorTypeName = _context.ActorTypes.Where(at => at.Id == a.ActorTypesId).Select(at => at.Name).FirstOrDefault(),
                GenderName = _context.Genders.Where(g => g.Id == a.GendersId).Select(g => g.Name).FirstOrDefault(),
                CountryName = _context.Countries.Where(c => c.Id == a.NationalityCountriesId).Select(c => c.Name).FirstOrDefault(),
                a.IsEnabled
            })
            .ToListAsync();

        return result;
    }
}
