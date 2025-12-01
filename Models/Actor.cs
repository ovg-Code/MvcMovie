using UUIDNext;

namespace ari2._0.Models;

public class Actor
{
    public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);
    public Guid? ActorTypesId { get; set; }
    public Guid? GendersId { get; set; }
    public Guid? NationalityCountriesId { get; set; }
    public string? Title { get; set; }
    public string? Prefix { get; set; }
    public string? Suffix { get; set; }
    public string? IsPep { get; set; }
    public string? FirstFirstName { get; set; }
    public string? SecondFirstName { get; set; }
    public string? LastFirstName { get; set; }
    public string? LastSecondName { get; set; }
    public DateTime? BirthdayAt { get; set; }
    public string? OtherData { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool? IsEnabled { get; set; }
}
