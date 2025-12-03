# Estandares de Comentarios en Codigo C#

Guia profesional para documentar codigo en el proyecto ARI 2.0, basada en las mejores practicas de Microsoft .NET.

Referencia: [Microsoft - XML Documentation Comments](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)

---

## Principio Fundamental

**Solo documentar lo necesario.** Segun Microsoft:
- Documentar clases e interfaces publicas con `<summary>`
- Documentar metodos publicos con logica compleja
- NO comentar cada propiedad si el nombre es autoexplicativo
- Evitar comentarios obvios que repiten el nombre del elemento

---

## 1. Comentarios XML (///)

### 1.1 Clases - Solo summary breve

```csharp
/// <summary>
/// Representa una persona o entidad en el sistema CRM.
/// </summary>
public class Actor
{
    public Guid Id { get; set; }
    public string? FirstFirstName { get; set; }
    // Las propiedades NO necesitan comentarios si son autoexplicativas
}
```

### 1.2 Propiedades - Solo si requieren explicacion especial

```csharp
public class Actor
{
    public Guid Id { get; set; }  // No necesita comentario
    
    /// <summary>
    /// Datos adicionales en formato JSON para informacion dinamica por proyecto.
    /// </summary>
    public string? OtherData { get; set; }  // SI necesita explicacion
}
```

### 1.3 Metodos - Documentar parametros y retorno

```csharp
/// <summary>
/// Crea un nuevo cliente en el sistema.
/// </summary>
/// <param name="customer">Entidad del cliente a crear.</param>
/// <returns>El cliente creado con su ID asignado.</returns>
public async Task<Customer> CreateCustomerAsync(Customer customer)
{
    // ...
}
```

### 1.4 Interfaces genericas

```csharp
/// <summary>
/// Interfaz generica para operaciones CRUD de acceso a datos.
/// </summary>
/// <typeparam name="T">Tipo de entidad.</typeparam>
public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
}
```

---

## 2. Comentarios en Linea (//)

Usar para explicar logica especifica dentro de metodos.

### 2.1 Logica de Negocio

```csharp
public async Task<Customer> CreateCustomerAsync(Customer customer)
{
    // Validacion: El cliente debe tener un actor asociado
    if (customer.ActorsId == null)
        throw new InvalidOperationException("El cliente debe estar asociado a un actor");

    // Asignar campos de auditoria antes de guardar
    customer.CreatedAt = DateTime.UtcNow;
    customer.CreatedBy = "system"; // TODO: Obtener del contexto de usuario

    return await _repository.AddAsync(customer);
}
```

### 2.2 Decisiones Tecnicas

```csharp
// Usamos UUIDv7 compatible con PostgreSQL para mejor rendimiento en indices
public Guid Id { get; set; } = Uuid.NewDatabaseFriendly(Database.PostgreSql);

// El campo OtherData almacena JSON para datos dinamicos por proyecto
// Limitado a 10-20 items segun requerimientos del modulo cliente
public string? OtherData { get; set; }
```

### 2.3 Reglas de Negocio

```csharp
// Regla de negocio: Un actor puede tener multiples identificaciones
// pero solo una puede estar marcada como principal
public async Task<IdentityCard> SetPrimaryIdentityCardAsync(Guid actorId, Guid cardId)
{
    // ...
}
```

---

## 3. Comentarios TODO y FIXME

### 3.1 TODO - Trabajo Pendiente

```csharp
// TODO: Implementar validacion de formato de email
// TODO: Agregar cache para consultas frecuentes
// TODO: Integrar con sistema de autenticacion centralizado
```

### 3.2 FIXME - Problemas Conocidos

```csharp
// FIXME: Esta consulta puede ser lenta con grandes volumenes de datos
// FIXME: Revisar manejo de timezone en fechas de auditoria
```

---

## 4. Tags XML Disponibles

| Tag | Uso |
|-----|-----|
| `<summary>` | Descripcion breve del elemento |
| `<remarks>` | Informacion adicional detallada |
| `<param name="x">` | Descripcion de parametro |
| `<returns>` | Descripcion del valor de retorno |
| `<exception cref="T">` | Excepciones que puede lanzar |
| `<value>` | Descripcion del valor de una propiedad |
| `<see cref="T"/>` | Referencia a otro tipo o miembro |
| `<seealso cref="T"/>` | Referencia relacionada |
| `<example>` | Ejemplo de uso |
| `<code>` | Bloque de codigo dentro de ejemplo |
| `<para>` | Parrafo dentro de remarks |
| `<c>` | Codigo en linea |

---

## 5. Que NO Hacer

### 5.1 Comentarios Obvios

```csharp
// MAL - Comentario obvio
i++; // Incrementa i en 1

// BIEN - Comentario con contexto
i++; // Avanzamos al siguiente registro del lote
```

### 5.2 Codigo Comentado sin Explicacion

```csharp
// MAL
// var oldLogic = CalculateOldWay();

// BIEN
// Logica anterior deshabilitada temporalmente mientras se valida el nuevo algoritmo
// var oldLogic = CalculateOldWay();
```

### 5.3 Comentarios Desactualizados

```csharp
// MAL - El comentario no refleja lo que hace el codigo
/// <summary>
/// Obtiene un cliente por ID.
/// </summary>
public async Task<IEnumerable<Customer>> GetAllCustomersAsync() // El metodo obtiene TODOS, no uno
```

---

## 6. Configuracion del Proyecto

Para habilitar la generacion de documentacion XML, agregar en el archivo `.csproj`:

```xml
<PropertyGroup>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <NoWarn>$(NoWarn);1591</NoWarn> <!-- Opcional: Suprimir warnings de documentacion faltante -->
</PropertyGroup>
```

---

## 7. Referencias

- [Documentacion XML en C# - Microsoft](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)
- [Tags Recomendados - Microsoft](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/recommended-tags)
- [Principios SOLID](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/architectural-principles)
