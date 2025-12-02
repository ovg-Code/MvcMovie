# Documentación del Proyecto ARI 2.0

Bienvenido a la documentación del proyecto ARI 2.0 - Sistema de Gestión de Clientes y Actores.

## 📊 Estado del Proyecto

**[Ver Estado Completo del Proyecto](./PROJECT_STATUS.md)** ⭐

| Componente | Estado |
|------------|--------|
| Arquitectura | ✅ Completada |
| Repository Pattern | ✅ 21/21 |
| Service Layer | ✅ 21/21 |
| Controllers | ✅ 21/21 |
| Documentación | ✅ Completa |

---

## 📚 Índice de Documentación

### Arquitectura
- **[Índice de Arquitectura](./architecture/README.md)** - Visión general de la arquitectura
- **[Repository Pattern y Service Layer](./architecture/REPOSITORY_PATTERN.md)** - Patrones de diseño implementados

### Base de Datos
- **[Documentación de Tablas](./database/)** - Esquemas y descripciones de todas las tablas
- **[database.json](../database.json)** - Fuente de verdad del esquema de base de datos

---

## 🏗️ Arquitectura del Proyecto

ARI 2.0 implementa una **arquitectura en capas** siguiendo las mejores prácticas de Microsoft:

```
┌─────────────────────────────────┐
│  Controllers (Presentation)     │  ← ASP.NET Core MVC
├─────────────────────────────────┤
│  Services (Business Logic)      │  ← Lógica de negocio
├─────────────────────────────────┤
│  Repositories (Data Access)     │  ← Acceso a datos
├─────────────────────────────────┤
│  Entity Framework Core          │  ← ORM
├─────────────────────────────────┤
│  PostgreSQL                      │  ← Base de datos
└─────────────────────────────────┘
```

**Lectura recomendada:** [Repository Pattern y Service Layer](./architecture/REPOSITORY_PATTERN.md)

---

## 🚀 Stack Tecnológico

| Componente | Tecnología | Versión |
|------------|-----------|---------|
| Framework | ASP.NET Core MVC | 8.0 |
| Lenguaje | C# | 12 |
| ORM | Entity Framework Core | 8.0 |
| Base de Datos | PostgreSQL | 16 |
| Patrón | Repository + Service Layer | - |
| Arquitectura | Layered (N-Tier) | - |

---

## 📁 Estructura del Proyecto

```
ari2.0/
├── Controllers/              # Controladores MVC (21 controladores)
│   ├── CustomersController.cs
│   ├── ActorsController.cs
│   └── ...
├── Services/                 # Capa de lógica de negocio (21 servicios)
│   ├── ICustomerService.cs
│   ├── CustomerService.cs
│   └── ...
├── Repositories/             # Capa de acceso a datos (21 repositorios)
│   ├── ICustomerRepository.cs
│   ├── CustomerRepository.cs
│   └── ...
├── Models/                   # Entidades del dominio (21 modelos)
│   ├── Customer.cs
│   ├── Actor.cs
│   └── ...
├── Data/                     # Contexto de Entity Framework
│   └── ApplicationDbContext.cs
├── Views/                    # Vistas Razor
│   ├── Customers/
│   ├── Actors/
│   └── ...
├── docs/                     # Documentación
│   ├── architecture/         # Documentación de arquitectura
│   ├── database/            # Documentación de base de datos
│   └── README.md            # Este archivo
├── database.json            # Esquema de base de datos (fuente de verdad)
└── Program.cs               # Punto de entrada de la aplicación
```

---

## 🎯 Principios de Diseño

### SOLID Principles
- ✅ **Single Responsibility:** Cada clase tiene una única responsabilidad
- ✅ **Open/Closed:** Abierto a extensión, cerrado a modificación
- ✅ **Liskov Substitution:** Las implementaciones son intercambiables
- ✅ **Interface Segregation:** Interfaces específicas y pequeñas
- ✅ **Dependency Inversion:** Dependemos de abstracciones

### Separation of Concerns
- ✅ **Controllers:** Solo coordinan el flujo HTTP
- ✅ **Services:** Contienen toda la lógica de negocio
- ✅ **Repositories:** Solo acceso a datos
- ✅ **Models:** Entidades del dominio

### Dependency Injection
- ✅ Uso del contenedor DI nativo de ASP.NET Core
- ✅ Registro con lifetime Scoped
- ✅ Inyección vía constructor

---

## 📊 Modelo de Datos

El proyecto gestiona 21 entidades principales:

### Entidades Core
- **Customer** - Clientes del sistema
- **Actor** - Actores (personas físicas/jurídicas)
- **ActorRelationship** - Relaciones entre actores

### Información de Contacto
- **Phone** - Teléfonos
- **Email** - Correos electrónicos
- **Address** - Direcciones

### Identificación
- **IdentityCard** - Documentos de identidad

### Ubicación Geográfica
- **Country** - Países
- **State** - Estados/Provincias
- **City** - Ciudades
- **Municipality** - Municipios
- **Neighborhood** - Barrios
- **ZipCode** - Códigos postales

### Tipos y Catálogos
- **ActorType** - Tipos de actores
- **AddressType** - Tipos de direcciones
- **CustomerPublicStatusType** - Estados públicos de clientes
- **Gender** - Géneros
- **IdentityCardType** - Tipos de documentos
- **PhoneType** - Tipos de teléfonos
- **RelationshipType** - Tipos de relaciones
- **SocialNetwork** - Redes sociales

**Ver:** [Documentación completa de base de datos](./database/)

---

## 🔧 Configuración y Desarrollo

### Requisitos Previos
- .NET 8.0 SDK
- PostgreSQL 16
- Visual Studio 2022 o VS Code

### Configuración de Base de Datos
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ari2;Username=postgres;Password=your_password"
  }
}
```

### Migraciones
```bash
# Crear migración
dotnet ef migrations add MigrationName

# Aplicar migración
dotnet ef database update
```

### Ejecutar el Proyecto
```bash
dotnet run
```

---

## 📖 Guías de Desarrollo

### Agregar una Nueva Entidad

1. **Crear el Modelo** en `Models/`
2. **Agregar al DbContext** en `Data/ApplicationDbContext.cs`
3. **Crear Repository Interface** en `Repositories/IEntityRepository.cs`
4. **Crear Repository Implementation** en `Repositories/EntityRepository.cs`
5. **Crear Service Interface** en `Services/IEntityService.cs`
6. **Crear Service Implementation** en `Services/EntityService.cs`
7. **Registrar en DI** en `Program.cs`
8. **Crear Controller** en `Controllers/EntityController.cs`
9. **Crear Views** en `Views/Entity/`

**Ver ejemplo completo:** [Repository Pattern - Implementación](./architecture/REPOSITORY_PATTERN.md#implementación-en-el-proyecto)

---

## 🧪 Testing

### Estructura de Tests
```
ari2.0.Tests/
├── Controllers/
├── Services/
└── Repositories/
```

### Ejemplo de Test
```csharp
[Test]
public async Task GetAllCustomers_ReturnsAllCustomers()
{
    // Arrange
    var mockRepo = new Mock<ICustomerRepository>();
    var service = new CustomerService(mockRepo.Object);
    
    // Act
    var result = await service.GetAllCustomersAsync();
    
    // Assert
    Assert.IsNotNull(result);
}
```

---

## 📚 Referencias Oficiales

### Microsoft Documentation
- [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)
- [Dependency Injection](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Repository Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)

### Libros Recomendados
- "Clean Architecture" por Robert C. Martin
- "Domain-Driven Design" por Eric Evans
- "Patterns of Enterprise Application Architecture" por Martin Fowler

---

## 🤝 Contribución

### Estándares de Código
- Seguir convenciones de C# de Microsoft
- Usar PascalCase para clases y métodos
- Usar camelCase para variables locales
- Interfaces empiezan con "I"
- Métodos async terminan con "Async"

### Commits
- Mensajes descriptivos en español
- Formato: `[Tipo] Descripción breve`
- Tipos: `[Feature]`, `[Fix]`, `[Refactor]`, `[Docs]`

---

## 📝 Licencia

[Especificar licencia del proyecto]

---

## 👥 Equipo

[Información del equipo de desarrollo]

---

**Última actualización:** Diciembre 2025
