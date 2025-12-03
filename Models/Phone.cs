using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Representa un numero de telefono asociado a un actor.
/// </summary>
public class Phone
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? ActorsId { get; set; }
    public Guid? PhoneTypesId { get; set; }
    public string? Number { get; set; }
    public string? Extension { get; set; }
    public bool? IsVerified { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
