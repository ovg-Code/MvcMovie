# Technology Stack

## Framework & Runtime
- **ASP.NET Core MVC** 8.0
- **C#** 12 with nullable reference types enabled
- **.NET SDK** 8.0

## Database & ORM
- **PostgreSQL** 16
- **Entity Framework Core** 9.0
- **Npgsql.EntityFrameworkCore.PostgreSQL** 9.0.2
- **EFCore.NamingConventions** 9.0.0 (snake_case mapping)

## Key Libraries
- **UUIDNext** 4.2.2 - Database-friendly UUID generation (UUIDv7 for PostgreSQL)
- **Microsoft.VisualStudio.Web.CodeGeneration.Design** 9.0.0

## Architecture Patterns
- **Repository Pattern**: Data access abstraction with generic base repository
- **Service Layer**: Business logic separation from controllers
- **Dependency Injection**: Native ASP.NET Core DI container with Scoped lifetime

## Common Commands

### Build & Run
```bash
# Build project
dotnet build

# Run application
dotnet run

# Run with watch (auto-reload)
dotnet watch run
```

### Database Migrations
```bash
# Create new migration
dotnet ef migrations add MigrationName

# Apply migrations to database
dotnet ef database update

# Remove last migration (if not applied)
dotnet ef migrations remove

# Generate SQL script
dotnet ef migrations script
```

### Code Generation
```bash
# Scaffold controller with views
dotnet aspnet-codegenerator controller -name EntityController -m Entity -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout
```

## Configuration
- Connection string in `appsettings.json` and `appsettings.Development.json`
- Snake_case naming convention for database (C# PascalCase → db snake_case)
- Implicit usings enabled
- Nullable reference types enforced
