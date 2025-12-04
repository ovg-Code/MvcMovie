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

        modelBuilder.Entity<Customer>()
            .Property(c => c.OtherData)
            .HasColumnType("jsonb");

        // Configurar foreign keys para Actor
        modelBuilder.Entity<Actor>()
            .HasOne(a => a.ActorType)
            .WithMany()
            .HasForeignKey(a => a.ActorTypesId);

        modelBuilder.Entity<Actor>()
            .HasOne(a => a.Gender)
            .WithMany()
            .HasForeignKey(a => a.GendersId);

        modelBuilder.Entity<Actor>()
            .HasOne(a => a.NationalityCountry)
            .WithMany()
            .HasForeignKey(a => a.NationalityCountriesId);

        // Configurar foreign keys explícitamente
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Actor)
            .WithMany()
            .HasForeignKey(c => c.ActorsId);

        modelBuilder.Entity<Customer>()
            .HasOne(c => c.CustomerPublicStatusType)
            .WithMany()
            .HasForeignKey(c => c.CustomerPublicStatusTypesId);

        modelBuilder.Entity<Phone>()
            .HasOne(p => p.Actor)
            .WithMany()
            .HasForeignKey(p => p.ActorsId);

        modelBuilder.Entity<Phone>()
            .HasOne(p => p.PhoneType)
            .WithMany()
            .HasForeignKey(p => p.PhoneTypesId);

        modelBuilder.Entity<Email>()
            .HasOne(e => e.Actor)
            .WithMany()
            .HasForeignKey(e => e.ActorsId);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.Actor)
            .WithMany()
            .HasForeignKey(a => a.ActorsId);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.AddressType)
            .WithMany()
            .HasForeignKey(a => a.AddressTypesId);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.ZipCode)
            .WithMany()
            .HasForeignKey(a => a.ZipCodesId);

        modelBuilder.Entity<IdentityCard>()
            .HasOne(i => i.Actor)
            .WithMany()
            .HasForeignKey(i => i.ActorsId);

        modelBuilder.Entity<IdentityCard>()
            .HasOne(i => i.IdentityCardType)
            .WithMany()
            .HasForeignKey(i => i.IdcardTypesId);

        modelBuilder.Entity<SocialNetwork>()
            .HasOne(s => s.Actor)
            .WithMany()
            .HasForeignKey(s => s.ActorsId);

        modelBuilder.Entity<ActorRelationship>()
            .HasOne(ar => ar.ParentActor)
            .WithMany()
            .HasForeignKey(ar => ar.ParentId);

        modelBuilder.Entity<ActorRelationship>()
            .HasOne(ar => ar.ChildActor)
            .WithMany()
            .HasForeignKey(ar => ar.ChildId);

        modelBuilder.Entity<ActorRelationship>()
            .HasOne(ar => ar.RelationshipType)
            .WithMany()
            .HasForeignKey(ar => ar.RelationshipTypesId);
    }
}
