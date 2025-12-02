using UUIDNext;

namespace ari2._0.Models;

public class Address
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? ActorsId { get; set; }
    public Guid? AddressTypesId { get; set; }
    public Guid? ZipCodesId { get; set; }
    public string? Street { get; set; }
    public string? Apartment { get; set; }
    public bool? IsVerified { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
