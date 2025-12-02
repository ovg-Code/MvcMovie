using Microsoft.EntityFrameworkCore;
using ari2._0.Data;
using ari2._0.Repositories;
using ari2._0.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention());

// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IIdentityCardRepository, IdentityCardRepository>();
builder.Services.AddScoped<IActorRelationshipRepository, ActorRelationshipRepository>();
builder.Services.AddScoped<IActorTypeRepository, ActorTypeRepository>();
builder.Services.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICustomerPublicStatusTypeRepository, CustomerPublicStatusTypeRepository>();
builder.Services.AddScoped<IGenderRepository, GenderRepository>();
builder.Services.AddScoped<IIdentityCardTypeRepository, IdentityCardTypeRepository>();
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
builder.Services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
builder.Services.AddScoped<IPhoneTypeRepository, PhoneTypeRepository>();
builder.Services.AddScoped<IRelationshipTypeRepository, RelationshipTypeRepository>();
builder.Services.AddScoped<ISocialNetworkRepository, SocialNetworkRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IZipCodeRepository, ZipCodeRepository>();

// Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IPhoneService, PhoneService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IIdentityCardService, IdentityCardService>();
builder.Services.AddScoped<IActorRelationshipService, ActorRelationshipService>();
builder.Services.AddScoped<IActorTypeService, ActorTypeService>();
builder.Services.AddScoped<IAddressTypeService, AddressTypeService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICustomerPublicStatusTypeService, CustomerPublicStatusTypeService>();
builder.Services.AddScoped<IGenderService, GenderService>();
builder.Services.AddScoped<IIdentityCardTypeService, IdentityCardTypeService>();
builder.Services.AddScoped<IMunicipalityService, MunicipalityService>();
builder.Services.AddScoped<INeighborhoodService, NeighborhoodService>();
builder.Services.AddScoped<IPhoneTypeService, PhoneTypeService>();
builder.Services.AddScoped<IRelationshipTypeService, RelationshipTypeService>();
builder.Services.AddScoped<ISocialNetworkService, SocialNetworkService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<IZipCodeService, ZipCodeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
