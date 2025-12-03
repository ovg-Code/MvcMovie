using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Catalogo de ciudades de un estado.
/// </summary>
public class City
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? StatesId { get; set; }
    public string? Name { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
