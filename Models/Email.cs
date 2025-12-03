using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Representa una direccion de correo electronico asociada a un actor.
/// </summary>
public class Email
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid ActorsId { get; set; }
    public string Definition { get; set; } = null!;
    public bool? IsNotification { get; set; }
    public bool? IsUnsuscribed { get; set; }
    public DateTime? UnsuscribedAt { get; set; }
    public bool? IsFailed { get; set; }
    public DateTime? FaliledAt { get; set; }
    public bool? IsPrimary { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
