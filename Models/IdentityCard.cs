using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Representa un documento de identificacion asociado a un actor.
/// </summary>
public class IdentityCard
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid ActorsId { get; set; }
    public Guid IdcardTypesId { get; set; }
    public string Idcard { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
