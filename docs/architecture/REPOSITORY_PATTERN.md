# Repository Pattern y Service Layer

## Tabla de Contenidos
- [Introducción](#introducción)
- [¿Qué es el Repository Pattern?](#qué-es-el-repository-pattern)
- [¿Qué es el Service Layer?](#qué-es-el-service-layer)
- [Arquitectura en Capas](#arquitectura-en-capas)
- [Interfaces vs Implementaciones](#interfaces-vs-implementaciones)
- [Implementación en el Proyecto](#implementación-en-el-proyecto)
- [Beneficios](#beneficios)
- [Referencias Oficiales](#referencias-oficiales)

---

## Introducción

Este documento explica la arquitectura en capas implementada en el proyecto ARI 2.0, siguiendo las mejores prácticas recomendadas por Microsoft para aplicaciones ASP.NET Core MVC empresariales.

**Importante:** Esta arquitectura NO cambia el hecho de que seguimos usando ASP.NET Core MVC. Solo organiza mejor el código siguiendo los principios SOLID.

---

## ¿Qué es el Repository Pattern?

El **Repository Pattern** es un patrón de diseño que actúa como una capa de abstracción entre la lógica de negocio y el acceso a datos.

### Propósito

1. **Aislar el acceso a datos:** Separa la lógica de negocio de los detalles de implementación de la base de datos
2. **Facilitar el testing:** Permite crear implementaciones falsas (mocks) para pruebas unitarias
3. **Flexibilidad:** Permite cambiar el ORM o la base de datos sin afectar el resto del código
4. **Reutilización:** Centraliza las operaciones de datos comunes

### Ejemplo Comparativo

#### ❌ Sin Repository Pattern
```csharp
public class CustomersController : Controller
{
    private readonly ApplicationDbContext _context;

    public async Task<IActionResult> Index()
    {
        // Controller acoplado directamente a Entity Framework
        return View(await _context.Customers.ToListAsync());
    }
}
```

**Problemas:**
- Controller conoce detalles de Entity Framework
- Difícil de testear (requiere base de datos real)
- Si cambias de ORM, debes modificar todos los controllers

#### ✅ Con Repository Pattern
```csharp
public class CustomersController : Controller
{
    private readonly ICustomerService _service;

    public async Task<IActionResult> Index()
    {
        // Controller solo conoce la interfaz del servicio
        return View(await _service.GetAllCustomersAsync());
    }
}
```

**Ventajas:**
- Controller desacoplado de la implementación de datos
- Fácil de testear con mocks
- Cambiar ORM solo requiere modificar el Repository

---

## ¿Qué es el Service Layer?

El **Service Layer** (Capa de Servicios) contiene la lógica de negocio de la aplicación.

### Propósito

1. **Centralizar lógica de negocio:** Validaciones, cálculos, reglas de negocio
2. **Coordinar operaciones:** Orquesta múltiples repositorios si es necesario
3. **Mantener controllers delgados:** Controllers solo coordinan, no contienen lógica
4. **Reutilización:** La misma lógica puede usarse desde diferentes controllers o APIs

### Responsabilidades

| Capa | Responsabilidad | Ejemplo |
|------|----------------|---------|
| **Controller** | Coordinar flujo HTTP | Recibir request, llamar servicio, retornar view |
| **Service** | Lógica de negocio | Validar datos, aplicar reglas, coordinar repositorios |
| **Repository** | Acceso a datos | CRUD operations, queries a base de datos |

---

## Arquitectura en Capas

```
┌─────────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                        │
│                   (Controllers + Views)                      │
│                                                              │
│  CustomersController.cs                                      │
│  - Recibe HTTP requests                                      │
│  - Valida ModelState                                         │
│  - Llama al Service                                          │
│  - Retorna Views                                             │
└──────────────────────────┬──────────────────────────────────┘
                           │ Dependency Injection
                           │ ICustomerService
┌──────────────────────────▼──────────────────────────────────┐
│                    BUSINESS LOGIC LAYER                      │
│                        (Services)                            │
│                                                              │
│  CustomerService.cs                                          │
│  - Lógica de negocio                                         │
│  - Validaciones complejas                                    │
│  - Coordina múltiples repositorios                           │
│  - Maneja transacciones                                      │
└──────────────────────────┬──────────────────────────────────┘
                           │ Dependency Injection
                           │ ICustomerRepository
┌──────────────────────────▼──────────────────────────────────┐
│                    DATA ACCESS LAYER                         │
│                      (Repositories)                          │
│                                                              │
│  CustomerRepository.cs                                       │
│  - CRUD operations                                           │
│  - Queries a base de datos                                   │
│  - Mapeo de entidades                                        │
└──────────────────────────┬──────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────────┐
│                    ENTITY FRAMEWORK CORE                     │
│                      (ApplicationDbContext)                  │
│                                                              │
│  - ORM (Object-Relational Mapping)                           │
│  - Conexión a PostgreSQL                                     │
│  - Migrations                                                │
└─────────────────────────────────────────────────────────────┘
```

---

## Interfaces vs Implementaciones

### ¿Por qué usar Interfaces?

Las interfaces (que empiezan con "I") definen **QUÉ** hace un componente, sin especificar **CÓMO** lo hace.

### Convención de Nomenclatura

```csharp
// INTERFAZ - Define el contrato
public interface ICustomerRepository  // ← Empieza con "I"
{
    Task<Customer> GetByIdAsync(Guid id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer> AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(Guid id);
}

// IMPLEMENTACIÓN - Código real
public class CustomerRepository : ICustomerRepository  // ← Implementa la interfaz
{
    private readonly ApplicationDbContext _context;
    
    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Customer> GetByIdAsync(Guid id)
    {
        return await _context.Customers.FindAsync(id);
    }
    
    // ... resto de implementación
}
```

### Ventajas de las Interfaces

#### 1. Dependency Injection
```csharp
// Program.cs - Registro de servicios
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

// Controller - Inyección automática
public class CustomersController : Controller
{
    private readonly ICustomerService _service;
    
    public CustomersController(ICustomerService service)
    {
        _service = service;  // ASP.NET Core inyecta automáticamente
    }
}
```

#### 2. Flexibilidad para Testing
```csharp
// Producción - Implementación real
public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    // ... usa PostgreSQL
}

// Testing - Implementación falsa
public class FakeCustomerRepository : ICustomerRepository
{
    private List<Customer> _customers = new();
    
    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        return Task.FromResult(_customers.AsEnumerable());
    }
}

// Test unitario
[Test]
public async Task GetAllCustomers_ReturnsAllCustomers()
{
    var fakeRepo = new FakeCustomerRepository();
    var service = new CustomerService(fakeRepo);
    
    var result = await service.GetAllCustomersAsync();
    
    Assert.IsNotNull(result);
}
```

#### 3. Cambiar Implementaciones sin Afectar el Código
```csharp
// Hoy: PostgreSQL con Entity Framework
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Mañana: MongoDB
builder.Services.AddScoped<ICustomerRepository, MongoCustomerRepository>();

// Pasado: API externa
builder.Services.AddScoped<ICustomerRepository, ApiCustomerRepository>();

// Controllers y Services NO cambian
```

---

## Implementación en el Proyecto

### Estructura de Archivos

```
ari2.0/
├── Controllers/
│   ├── CustomersController.cs       # Usa ICustomerService
│   ├── ActorsController.cs          # Usa IActorService
│   └── ...
├── Services/
│   ├── Interfaces/                  # (Opcional, pueden estar en raíz)
│   ├── ICustomerService.cs          # Interfaz del servicio
│   ├── CustomerService.cs           # Implementación del servicio
│   ├── IActorService.cs
│   ├── ActorService.cs
│   └── ...
├── Repositories/
│   ├── Interfaces/                  # (Opcional, pueden estar en raíz)
│   ├── IRepository.cs               # Interfaz genérica
│   ├── Repository.cs                # Implementación genérica
│   ├── ICustomerRepository.cs       # Interfaz específica
│   ├── CustomerRepository.cs        # Implementación específica
│   └── ...
├── Models/
│   ├── Customer.cs
│   ├── Actor.cs
│   └── ...
└── Data/
    └── ApplicationDbContext.cs
```

### Ejemplo Completo: Customer

#### 1. Interfaz del Repository
```csharp
// Repositories/ICustomerRepository.cs
namespace ari2._0.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    Task<Customer?> GetCustomerWithDetailsAsync(Guid id);
}
```

#### 2. Implementación del Repository
```csharp
// Repositories/CustomerRepository.cs
namespace ari2._0.Repositories;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
    {
        return await _context.Customers
            .Where(c => c.IsEnabled)
            .ToListAsync();
    }
    
    public async Task<Customer?> GetCustomerWithDetailsAsync(Guid id)
    {
        return await _context.Customers
            .Include(c => c.Actor)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
```

#### 3. Interfaz del Service
```csharp
// Services/ICustomerService.cs
namespace ari2._0.Services;

public interface ICustomerService
{
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
    Task<IEnumerable<Customer>> GetActiveCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(Guid id);
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(Guid id);
    Task<bool> CustomerExistsAsync(Guid id);
}
```

#### 4. Implementación del Service
```csharp
// Services/CustomerService.cs
namespace ari2._0.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    
    public CustomerService(ICustomerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        return await _repository.GetAllAsync();
    }
    
    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        // Lógica de negocio: generar ID, validaciones, etc.
        customer.Id = Guid.NewGuid();
        customer.CreatedAt = DateTime.UtcNow;
        
        return await _repository.AddAsync(customer);
    }
    
    // ... resto de métodos
}
```

#### 5. Controller
```csharp
// Controllers/CustomersController.cs
namespace ari2._0.Controllers;

public class CustomersController : Controller
{
    private readonly ICustomerService _service;
    
    public CustomersController(ICustomerService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _service.GetAllCustomersAsync());
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateCustomerAsync(customer);
            return RedirectToAction(nameof(Index));
        }
        return View(customer);
    }
    
    // ... resto de acciones
}
```

#### 6. Registro en Program.cs
```csharp
// Program.cs
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
```

### Patrón Genérico

Para evitar duplicación de código, usamos interfaces y clases genéricas:

```csharp
// Repositories/IRepository.cs
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}

// Repositories/Repository.cs
public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }
    
    // ... implementación genérica
}
```

---

## Beneficios

### 1. Separación de Responsabilidades (SOLID)
- **S**ingle Responsibility: Cada clase tiene una única responsabilidad
- **O**pen/Closed: Abierto a extensión, cerrado a modificación
- **L**iskov Substitution: Las implementaciones son intercambiables
- **I**nterface Segregation: Interfaces específicas y pequeñas
- **D**ependency Inversion: Dependemos de abstracciones, no de implementaciones

### 2. Testabilidad
```csharp
// Test sin base de datos real
[Test]
public async Task CreateCustomer_ValidCustomer_ReturnsCustomer()
{
    // Arrange
    var mockRepo = new Mock<ICustomerRepository>();
    var service = new CustomerService(mockRepo.Object);
    var customer = new Customer { Name = "Test" };
    
    // Act
    var result = await service.CreateCustomerAsync(customer);
    
    // Assert
    Assert.IsNotNull(result);
    mockRepo.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
}
```

### 3. Mantenibilidad
- Código organizado y fácil de entender
- Cambios localizados (modificar Repository no afecta Controller)
- Fácil agregar nuevas funcionalidades

### 4. Escalabilidad
- Fácil agregar caching en el Service Layer
- Fácil agregar logging/auditoría
- Fácil implementar transacciones complejas

### 5. Flexibilidad
```csharp
// Cambiar de PostgreSQL a MongoDB
public class MongoCustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _collection;
    
    // Implementación con MongoDB
    // Controllers y Services NO cambian
}
```

---

## Referencias Oficiales

### Documentación de Microsoft

1. **ASP.NET Core MVC Overview**
   - https://learn.microsoft.com/en-us/aspnet/core/mvc/overview
   - Introducción oficial a ASP.NET Core MVC

2. **Dependency Injection in ASP.NET Core**
   - https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection
   - Cómo funciona la inyección de dependencias

3. **Repository Pattern**
   - https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design
   - Diseño de la capa de persistencia

4. **Clean Architecture**
   - https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
   - Arquitecturas comunes para aplicaciones web

5. **SOLID Principles**
   - https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles
   - Principios arquitectónicos

6. **Testing in ASP.NET Core**
   - https://learn.microsoft.com/en-us/aspnet/core/test/
   - Guías de testing

### Libros Recomendados

- **"Clean Architecture" por Robert C. Martin**
- **"Domain-Driven Design" por Eric Evans**
- **"Patterns of Enterprise Application Architecture" por Martin Fowler**

---

## Conclusión

La implementación del Repository Pattern y Service Layer en ARI 2.0 sigue las mejores prácticas oficiales de Microsoft para aplicaciones ASP.NET Core MVC empresariales.

**Puntos Clave:**
- ✅ Sigue siendo ASP.NET Core MVC
- ✅ Código más limpio y organizado
- ✅ Fácil de testear y mantener
- ✅ Preparado para escalar
- ✅ Sigue estándares de la industria

Esta arquitectura nos permite:
- Cambiar la base de datos sin afectar la lógica de negocio
- Testear sin necesidad de base de datos real
- Mantener controllers delgados y enfocados
- Reutilizar lógica de negocio
- Cumplir con principios SOLID
