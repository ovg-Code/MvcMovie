# Documentación de Desarrollo

Esta carpeta contiene guías y documentación para desarrolladores del proyecto ARI 2.0.

## Documentos Disponibles

### 1. [Guía de Implementación](./IMPLEMENTATION_GUIDE.md)
Guía paso a paso para implementar nuevas funcionalidades siguiendo los patrones del proyecto:
- Cómo agregar nuevas entidades
- Cómo crear Repositories y Services
- Cómo crear Controllers
- Ejemplos de código completos

**Lectura recomendada para:** Desarrolladores que van a agregar nuevas funcionalidades

### 2. [Scaffolding Automático](./SCAFFOLDING_AUTOMATICO.md)
Documentación sobre el sistema de scaffolding automático para generar código.

### 3. [Historial de Scaffolding](./SCAFFOLDING_HISTORY.md)
Registro de todas las operaciones de scaffolding realizadas en el proyecto.

### 4. [Traductor de Nomenclatura](./TRADUCTOR_NOMENCLATURA.md)
Guía para la conversión entre nomenclaturas (PascalCase ↔ snake_case).

---

## Flujo de Desarrollo

### 1. Planificación
- Revisar requisitos en `/docs/requirements/`
- Verificar esquema de base de datos en `database.json`
- Revisar documentación de arquitectura en `/docs/architecture/`

### 2. Implementación
- Seguir la [Guía de Implementación](./IMPLEMENTATION_GUIDE.md)
- Usar el patrón Repository + Service Layer
- Mantener controllers delgados

### 3. Testing
- Crear tests unitarios para Services
- Crear tests de integración para Repositories
- Verificar con `dotnet test`

### 4. Documentación
- Actualizar documentación de base de datos si aplica
- Documentar decisiones de diseño importantes
- Actualizar README si es necesario

---

## Estándares de Código

### Nomenclatura
- **Clases:** PascalCase (`CustomerService`)
- **Interfaces:** PascalCase con prefijo "I" (`ICustomerService`)
- **Métodos:** PascalCase (`GetAllCustomersAsync`)
- **Variables locales:** camelCase (`customerList`)
- **Métodos async:** Sufijo "Async" (`CreateCustomerAsync`)

### Estructura de Archivos
```
Controllers/
  EntityController.cs
Services/
  IEntityService.cs
  EntityService.cs
Repositories/
  IEntityRepository.cs
  EntityRepository.cs
Models/
  Entity.cs
```

### Dependency Injection
```csharp
// Program.cs
builder.Services.AddScoped<IEntityRepository, EntityRepository>();
builder.Services.AddScoped<IEntityService, EntityService>();
```

---

## Herramientas de Desarrollo

### Entity Framework Core
```bash
# Crear migración
dotnet ef migrations add MigrationName

# Aplicar migración
dotnet ef database update

# Revertir migración
dotnet ef database update PreviousMigrationName

# Eliminar última migración
dotnet ef migrations remove
```

### Build y Run
```bash
# Compilar
dotnet build

# Ejecutar
dotnet run

# Watch mode (auto-reload)
dotnet watch run
```

### Testing
```bash
# Ejecutar todos los tests
dotnet test

# Ejecutar tests con cobertura
dotnet test /p:CollectCoverage=true
```

---

## Referencias Rápidas

- [Arquitectura del Proyecto](../architecture/REPOSITORY_PATTERN.md)
- [Documentación de Base de Datos](../database/)
- [Requisitos del Sistema](../requirements/)
