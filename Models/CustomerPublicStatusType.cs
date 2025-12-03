using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Catalogo de estados publicos del cliente.
/// </summary>
public class CustomerPublicStatusType
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public string? Name { get; set; }
    public string? SystemName { get; set; }
    public int? Order { get; set; }
    public bool? IsPrivate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
