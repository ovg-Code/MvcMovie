# Traductor de Nomenclatura: PascalCase ↔ snake_case

## ¿Qué es el traductor?

Es un mecanismo automático que convierte nombres entre dos convenciones:
- **C# (PascalCase)**: `ActorType`, `CreatedAt`, `CountriesId`
- **PostgreSQL (snake_case)**: `actor_type`, `created_at`, `countries_id`

**Implementación actual**: Usamos la librería oficial **EFCore.NamingConventions** recomendada por Microsoft.

## ¿Por qué lo necesitamos?

### Problema Original

**En C# escribimos:**
```csharp
public class ActorType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CountriesId { get; set; }
}
```

**En PostgreSQL tenemos:**
```sql
CREATE TABLE actor_types (
    id UUID,
    name VARCHAR,
    created_at TIMESTAMP,
    countries_id UUID
);
```

**Sin traductor**, teníamos que mapear MANUALMENTE cada campo:

```csharp
modelBuilder.Entity<ActorType>(entity =>
{
    entity.ToTable("actor_types");
    entity.Property(e => e.Id).HasColumnName("id");
    entity.Property(e => e.Name).HasColumnName("name");
    entity.Property(e => e.CreatedAt).HasColumnName("created_at");
    entity.Property(e => e.CountriesId).HasColumnName("countries_id");
});
```

Esto para **CADA TABLA** = mucho código repetitivo.

## Solución: EFCore.NamingConventions (Librería Oficial)

### Instalación

```bash
dotnet add package EFCore.NamingConventions --version 8.0.3
```

### Configuración en Program.cs

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention());
```

**¡Eso es todo!** Una sola línea activa la traducción automática para todas las tablas y columnas.

### ApplicationDbContext

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Country> Countries { get; set; }
    public DbSet<ActorType> ActorTypes { get; set; }
    // ... más DbSets
    
    // No necesita OnModelCreating para naming conventions
}
```

### ¿Cómo funciona el traductor?

**Paso a paso:**

1. **ActorType** (clase C#)
   - Encuentra 'T' mayúscula en posición 5
   - Inserta "_" antes: "Actor_Type"
   - Convierte a minúsculas: "actor_type"

2. **CreatedAt** (propiedad C#)
   - Encuentra 'A' mayúscula en posición 7
   - Inserta "_" antes: "Created_At"
   - Convierte a minúsculas: "created_at"

3. **CountriesId** (propiedad C#)
   - Encuentra 'I' mayúscula en posición 9
   - Inserta "_" antes: "Countries_Id"
   - Convierte a minúsculas: "countries_id"

### Ejemplos de Traducción

| C# (PascalCase) | PostgreSQL (snake_case) |
|-----------------|-------------------------|
| `Country` | `country` |
| `ActorType` | `actor_type` |
| `PhoneType` | `phone_type` |
| `AddressType` | `address_type` |
| `Id` | `id` |
| `Name` | `name` |
| `CreatedAt` | `created_at` |
| `UpdatedAt` | `updated_at` |
| `CreatedBy` | `created_by` |
| `IsEnabled` | `is_enabled` |
| `CountriesId` | `countries_id` |
| `StatesId` | `states_id` |

## Ventajas de EFCore.NamingConventions

### ✅ Antes (Sin Traductor)
```csharp
// 150+ líneas de código repetitivo
modelBuilder.Entity<Country>(entity => {
    entity.ToTable("countries");
    entity.Property(e => e.Id).HasColumnName("id");
    entity.Property(e => e.Name).HasColumnName("name");
    // ... 8 propiedades más
});

modelBuilder.Entity<ActorType>(entity => {
    entity.ToTable("actor_types");
    entity.Property(e => e.Id).HasColumnName("id");
    // ... 8 propiedades más
});

// Repetir para 10 tablas...
```

### ✅ Después (Con EFCore.NamingConventions)
```csharp
// 1 línea en Program.cs - funciona para TODAS las tablas
.UseSnakeCaseNamingConvention()
```

### Beneficios

1. **Mínimo código**: 1 línea vs 150+ líneas
2. **Solución oficial**: Mantenida por el equipo de EF Core
3. **Probada en producción**: Usada por miles de proyectos
4. **Automático**: Agregar nueva tabla no requiere configuración
5. **Sin errores**: Maneja casos edge que implementaciones manuales no cubren
6. **Actualizaciones**: Compatible con nuevas versiones de EF Core
7. **Mantenible**: Sin código personalizado que mantener

### Comparación: Manual vs Librería

| Aspecto | Implementación Manual | EFCore.NamingConventions |
|---------|----------------------|--------------------------|
| Líneas de código | ~20 líneas | 1 línea |
| Mantenimiento | Requiere actualización manual | Automático con EF Core |
| Casos edge | Puede fallar | Todos cubiertos |
| Soporte oficial | No | Sí (Microsoft) |
| Confiabilidad | Depende de implementación | Probado en producción |

## Cumplimiento de Buenas Prácticas

### ✅ Lo que dice Jose Luis Alvaro:
> "Las buenas prácticas se hacen en camel case y debe haber un traductor que por cada _ lo quita y convierte a mayúscula la siguiente"

**Nuestro traductor hace exactamente eso, pero en reversa:**
- C# → PostgreSQL: `ActorType` → `actor_type`
- PostgreSQL → C# (lectura): `actor_type` → `ActorType`

### ✅ Lo que dice Jorge Castillo:
> "en C# es mas habitual el camel case nombrar entidades pero tambien creo que podemos mantener el mismo estandar definido en la base de datos"

**Con el traductor mantenemos AMBOS estándares:**
- En C#: `ActorType`, `CreatedAt` (PascalCase)
- En PostgreSQL: `actor_type`, `created_at` (snake_case)

## Resumen

**EFCore.NamingConventions** es la solución oficial recomendada por Microsoft:
- Traduce automáticamente entre C# (PascalCase) y PostgreSQL (snake_case)
- Una sola línea de configuración: `.UseSnakeCaseNamingConvention()`
- Mantenida por el equipo de EF Core
- Probada en miles de proyectos en producción
- Compatible con EF Core 3-9

**Resultado**: Código limpio, mantenible y que sigue las mejores prácticas de la industria.

## Referencias

- [EFCore.NamingConventions en GitHub](https://github.com/efcore/EFCore.NamingConventions)
- [EFCore.NamingConventions en NuGet](https://www.nuget.org/packages/EFCore.NamingConventions)
- [Documentación oficial de Microsoft](https://learn.microsoft.com/en-us/ef/core/extensions/#efcorenamingconventions)
