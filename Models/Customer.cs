using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Representa un cliente o contribuyente en el sistema CRM.
/// Extiende la informacion de Actor con datos de la relacion comercial.
/// </summary>
public class Customer
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? ActorsId { get; set; }
    public Guid? CustomerPublicStatusTypesId { get; set; }
    public bool? IsAgentRetention { get; set; }
    public bool? IsLeasing { get; set; }
    
    /// <summary>
    /// Datos adicionales en formato JSON para informacion dinamica por proyecto.
    /// </summary>
    public string? OtherData { get; set; }
    
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
