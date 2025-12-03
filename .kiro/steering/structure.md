# Project Structure

## Layered Architecture

```
Controllers → Services → Repositories → DbContext → PostgreSQL
```

Each layer has a single responsibility and depends only on the layer below it.

## Folder Organization

### `/Controllers`
MVC controllers - thin coordinators that delegate to services. Never access DbContext directly.
- Use dependency injection for services
- Handle HTTP concerns (routing, model binding, validation)
- Return views or redirect actions

### `/Services`
Business logic layer with interfaces and implementations.
- `IEntityService.cs` - Service interface
- `EntityService.cs` - Service implementation
- Contains validation, business rules, and orchestration
- Depends on repositories, never on DbContext directly

### `/Repositories`
Data access layer with generic base and specific implementations.
- `IRepository.cs` - Generic repository interface
- `Repository.cs` - Generic repository base class
- `IEntityRepository.cs` - Entity-specific interface
- `EntityRepository.cs` - Entity-specific implementation
- Only layer that queries the database

### `/Models`
Entity classes representing database tables.
- Plain C# classes with properties
- Use `Guid` for primary keys (UUIDv7)
- Nullable reference types for optional fields
- Audit fields: `CreatedAt`, `CreatedBy`, `UpdatedAt`, `UpdatedBy`, `IsEnabled`

### `/Data`
Entity Framework Core DbContext.
- `ApplicationDbContext.cs` - Single DbContext for all entities
- DbSet properties for each entity
- Configured with snake_case naming convention

### `/Views`
Razor views organized by controller.
- `/Views/EntityName/` - Folder per entity
- Standard CRUD views: `Index.cshtml`, `Create.cshtml`, `Edit.cshtml`, `Details.cshtml`, `Delete.cshtml`
- `/Views/Shared/` - Shared layouts and partials
- `_Layout.cshtml` - Main layout
- `_LayoutDashboard.cshtml` - Dashboard layout

### `/wwwroot`
Static files (CSS, JS, images).
- `/wwwroot/css/` - Stylesheets
- `/wwwroot/js/` - JavaScript files
- `/wwwroot/images/` - Image assets

### `/docs`
Comprehensive project documentation.
- `/docs/architecture/` - Architecture patterns and design decisions
- `/docs/database/` - Database schema documentation per table
- `/docs/development/` - Coding standards and implementation guides
- `/docs/requirements/` - Business requirements and specifications

### `/scripts`
Automation scripts for code generation and database operations.

## Naming Conventions

### C# Code
- **Classes/Interfaces**: PascalCase (`CustomerService`, `ICustomerRepository`)
- **Methods**: PascalCase (`GetAllCustomersAsync`)
- **Properties**: PascalCase (`FirstName`, `IsEnabled`)
- **Private fields**: _camelCase (`_customerRepository`)
- **Parameters/locals**: camelCase (`customer`, `customerId`)
- **Interfaces**: Prefix with `I` (`ICustomerService`)
- **Async methods**: Suffix with `Async` (`CreateCustomerAsync`)

### Database
- **Tables**: snake_case plural (`customers`, `actor_relationships`)
- **Columns**: snake_case (`first_name`, `is_enabled`)
- **Foreign keys**: `{table}_id` (`actors_id`, `customer_public_status_types_id`)

## Dependency Registration Pattern

All repositories and services must be registered in `Program.cs`:

```csharp
// Repositories
builder.Services.AddScoped<IEntityRepository, EntityRepository>();

// Services  
builder.Services.AddScoped<IEntityService, EntityService>();
```

## Entity Count
Currently 21 entities fully implemented with complete Repository/Service/Controller layers:
- Core: Customer, Actor, ActorRelationship
- Contact: Phone, Email, Address, SocialNetwork, IdentityCard
- Geography: Country, State, City, Municipality, Neighborhood, ZipCode
- Types/Catalogs: ActorType, AddressType, PhoneType, IdentityCardType, RelationshipType, CustomerPublicStatusType, Gender
