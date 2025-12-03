using Microsoft.EntityFrameworkCore;
using ari2._0.Models;

namespace ari2._0.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public override int SaveChanges()
    {
        ConvertDatesToUtc();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDatesToUtc();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDatesToUtc()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.ClrType == typeof(DateTime) || property.Metadata.ClrType == typeof(DateTime?))
                {
                    if (property.CurrentValue != null && property.CurrentValue is DateTime dateTime)
                    {
                        if (dateTime.Kind == DateTimeKind.Unspecified)
                        {
                            property.CurrentValue = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
                        }
                        else if (dateTime.Kind == DateTimeKind.Local)
                        {
                            property.CurrentValue = dateTime.ToUniversalTime();
                        }
                    }
                }
            }
        }
    }

    // Catálogos
    public DbSet<Country> Countries { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<ActorType> ActorTypes { get; set; }
    public DbSet<PhoneType> PhoneTypes { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Municipality> Municipalities { get; set; }
    public DbSet<Neighborhood> Neighborhoods { get; set; }
    public DbSet<ZipCode> ZipCodes { get; set; }
    public DbSet<IdentityCardType> IdentityCardTypes { get; set; }
    public DbSet<RelationshipType> RelationshipTypes { get; set; }
    public DbSet<CustomerPublicStatusType> CustomerPublicStatusTypes { get; set; }

    // Tablas principales
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<IdentityCard> IdentityCards { get; set; }
    public DbSet<ActorRelationship> ActorRelationships { get; set; }
    public DbSet<SocialNetwork> SocialNetworks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>()
            .Property(a => a.OtherData)
            .HasColumnType("jsonb");
    }
}
