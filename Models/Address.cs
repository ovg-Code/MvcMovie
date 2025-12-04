using UUIDNext;

namespace ari2._0.Models;

/// <summary>
/// Representa una direccion fisica asociada a un actor.
/// </summary>
public class Address
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? ActorsId { get; set; }
    public Guid? AddressTypesId { get; set; }
    public Guid? ZipCodesId { get; set; }
    public string? Street { get; set; }
    public string? Apartment { get; set; }
    public bool? IsVerified { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }

    // Navigation properties
    public virtual Actor? Actor { get; set; }
    public virtual AddressType? AddressType { get; set; }
    public virtual ZipCode? ZipCode { get; set; }
}
