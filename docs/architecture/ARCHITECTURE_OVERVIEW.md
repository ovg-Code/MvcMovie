# Arquitectura del Proyecto ARI 2.0

## 📐 Patrón de Arquitectura

Este proyecto sigue las **mejores prácticas de ASP.NET Core MVC** con:
- **Repository Pattern** - Abstracción de acceso a datos
- **Service Layer** - Lógica de negocio centralizada
- **Dependency Injection** - Inyección de dependencias nativa de .NET

## 🏗️ Estructura de Capas

```
┌─────────────────────────────────────┐
│         Controllers                 │  ← Coordinan requests
│  (Delgados, sin lógica de negocio) │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│           Services                  │  ← Lógica de negocio
│  (Validaciones, cálculos, reglas)  │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│         Repositories                │  ← Acceso a datos
│  (Queries, CRUD operations)         │
└─────────────────────────────────────┘
              ↓
┌─────────────────────────────────────┐
│      DbContext (EF Core)            │  ← ORM
└─────────────────────────────────────┘
              ↓
         PostgreSQL
```

## 📁 Estructura de Carpetas

```
ari2.0/
├── Controllers/          # Controladores MVC (coordinan)
├── Services/            # Lógica de negocio
│   ├── ICustomerService.cs
│   └── CustomerService.cs
├── Repositories/        # Acceso a datos
│   ├── IRepository.cs
│   ├── Repository.cs
│   ├── ICustomerRepository.cs
│   └── CustomerRepository.cs
├── Models/              # Entidades de base de datos
├── Data/                # DbContext
├── Views/               # Vistas Razor
└── wwwroot/             # Archivos estáticos
```

## 🔄 Flujo de una Request

### Ejemplo: Crear un Customer

```
1. Usuario → POST /Customers/Create
              ↓
2. CustomersController.Create(customer)
   - Valida ModelState
   - Llama al Service
              ↓
3. CustomerService.CreateCustomerAsync(customer)
   - Aplica reglas de negocio
   - Validaciones adicionales
   - Llama al Repository
              ↓
4. CustomerRepository.AddAsync(customer)
   - Ejecuta query en DB
   - Guarda cambios
              ↓
5. PostgreSQL
   - Inserta registro
              ↓
6. Respuesta ← Controller ← Service ← Repository
```

## 💉 Dependency Injection

### Registro en Program.cs

```csharp
// Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Services
builder.Services.AddScoped<ICustomerService, CustomerService>();
```

### Uso en Controller

```csharp
public class CustomersController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IActionResult> Index()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return View(customers);
    }
}
```

## ✅ Beneficios de esta Arquitectura

### 1. **Separación de Responsabilidades**
- Controllers: Solo coordinan
- Services: Lógica de negocio
- Repositories: Acceso a datos

### 2. **Testeable**
- Puedes hacer mock de Services y Repositories
- Unit tests sin base de datos

### 3. **Mantenible**
- Código organizado y limpio
- Fácil encontrar y modificar lógica

### 4. **Reutilizable**
- Services pueden usarse desde múltiples controllers
- Repositories centralizan queries

### 5. **Flexible**
- Fácil cambiar de ORM (EF Core → Dapper)
- Fácil agregar caché, logging, etc.

## 🎯 Ejemplo Completo: Customer

### 1. Repository Interface
```csharp
public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetActiveCustomersAsync();
}
```

### 2. Repository Implementation
```csharp
public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
    {
        return await _dbSet.Where(c => c.IsEnabled == true).ToListAsync();
    }
}
```

### 3. Service Interface
```csharp
public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<Customer> CreateCustomerAsync(Customer customer);
}
```

### 4. Service Implementation
```csharp
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        // Validaciones de negocio aquí
        return await _repository.AddAsync(customer);
    }
}
```

### 5. Controller
```csharp
public class CustomersController : Controller
{
    private readonly ICustomerService _service;

    public async Task<IActionResult> Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateCustomerAsync(customer);
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }
}
```

## 📚 Referencias

- [ASP.NET Core Dependency Injection](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
- [Repository Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- [Service Layer Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-application-layer-implementation-web-api)

## ✅ Estado de Implementación

### Completado

1. ✅ **Repository Pattern implementado para TODAS las entidades (21/21)**
   - Customer, Actor, Phone, Email, Address, IdentityCard
   - ActorRelationship, ActorType, AddressType
   - City, Country, CustomerPublicStatusType
   - Gender, IdentityCardType, Municipality
   - Neighborhood, PhoneType, RelationshipType
   - SocialNetwork, State, ZipCode

2. ✅ **Service Layer implementado para TODAS las entidades (21/21)**
   - Interfaces y implementaciones completas
   - Lógica de negocio centralizada
   - Todos los servicios registrados en DI

3. ✅ **Controllers refactorizados (21/21)**
   - Todos usan Service Layer
   - Ninguno accede directamente a DbContext
   - Controllers delgados y enfocados

4. ✅ **Dependency Injection configurado**
   - Todos los servicios registrados en Program.cs
   - Lifetime Scoped apropiado

5. ✅ **Documentación completa**
   - Repository Pattern explicado
   - Service Layer documentado
   - Guías de implementación
   - Referencias oficiales de Microsoft

### Próximos Pasos Sugeridos

1. ⏳ Agregar validaciones avanzadas en Services
2. ⏳ Implementar Unit of Work para transacciones complejas
3. ⏳ Agregar logging centralizado
4. ⏳ Implementar manejo de errores global
5. ⏳ Agregar tests unitarios para Services
6. ⏳ Agregar tests de integración para Repositories
