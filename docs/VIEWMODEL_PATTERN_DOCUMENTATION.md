# Patrón ViewModel para Formularios Create - Documentación

## 📋 Resumen

Implementación del patrón **ViewModel con SelectListItem** para poblar dropdowns en formularios ASP.NET Core MVC, siguiendo la **mejor práctica oficial de Microsoft** en lugar de usar ViewBag/ViewData.

---

## 🎯 Mejor Práctica de Microsoft

### Cita Oficial de la Documentación

> **"Note: We don't recommend using `ViewBag` or `ViewData` with the Select Tag Helper. A view model is more robust at providing MVC metadata and generally less problematic."**
>
> — [Microsoft Learn: Tag Helpers in forms](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-10.0#the-select-tag-helper)

---

## ❌ Antipatrón: ViewBag/ViewData

### Código Incorrecto (No Recomendado)

```csharp
// Controller
public IActionResult Create()
{
    ViewBag.ActorsId = new SelectList(_context.Actors, "Id", "Name");
    return View();
}
```

```cshtml
<!-- Vista -->
<select asp-for="ActorsId" asp-items="ViewBag.ActorsId" class="form-select"></select>
```

### Problemas

- ❌ **Sin type-safety**: Errores solo en runtime
- ❌ **Sin IntelliSense**: No hay autocompletado
- ❌ **Propenso a errores**: Typos en nombres de propiedades
- ❌ **Difícil de mantener**: Cambios requieren buscar en múltiples archivos
- ❌ **Sin validación en compilación**: Refactorings pueden romper código

---

## ✅ Patrón Correcto: ViewModel

### 1. Crear ViewModel

**Ubicación:** `/ViewModels/CustomerCreateViewModel.cs`

```csharp
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ari2._0.ViewModels;

/// <summary>
/// ViewModel para el formulario de creación de clientes.
/// Sigue la mejor práctica de Microsoft: usar ViewModels en lugar de ViewBag/ViewData
/// para poblar dropdowns con SelectListItem.
/// Referencia: https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms
/// </summary>
public class CustomerCreateViewModel
{
    // Propiedades del modelo Customer
    public Guid? ActorsId { get; set; }
    public Guid? CustomerPublicStatusTypesId { get; set; }
    public bool? IsAgentRetention { get; set; }
    public bool? IsLeasing { get; set; }
    public bool? IsEnabled { get; set; }
    public string? OtherData { get; set; }
    
    // Listas para poblar los dropdowns (mejor práctica vs ViewBag)
    public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
    public IEnumerable<SelectListItem> CustomerPublicStatusTypes { get; set; } = new List<SelectListItem>();
}
```

### 2. Actualizar Controller

**Ubicación:** `/Controllers/CustomersController.cs`

```csharp
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ari2._0.ViewModels;
using ari2._0.Data;

public class CustomersController : Controller
{
    private readonly ICustomerService _service;
    private readonly ApplicationDbContext _context;

    public CustomersController(ICustomerService service, ApplicationDbContext context)
    {
        _service = service;
        _context = context;
    }

    /// <summary>
    /// GET: Customers/Create
    /// Pobla los dropdowns usando ViewModel (mejor práctica vs ViewBag/ViewData).
    /// Filtra solo registros activos (IsEnabled = true) para mejor UX.
    /// </summary>
    public async Task<IActionResult> Create()
    {
        var viewModel = new CustomerCreateViewModel
        {
            Actors = await _context.Actors
                .Where(a => a.IsEnabled == true)
                .Select(a => new SelectListItem 
                { 
                    Value = a.Id.ToString(), 
                    Text = a.FirstFirstName + " " + a.LastFirstName
                })
                .ToListAsync(),
                
            CustomerPublicStatusTypes = await _context.CustomerPublicStatusTypes
                .Where(c => c.IsEnabled == true)
                .Select(c => new SelectListItem 
                { 
                    Value = c.Id.ToString(), 
                    Text = c.Name 
                })
                .ToListAsync()
        };
        
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CustomerCreateViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var customer = new Customer
            {
                ActorsId = viewModel.ActorsId,
                CustomerPublicStatusTypesId = viewModel.CustomerPublicStatusTypesId,
                IsAgentRetention = viewModel.IsAgentRetention,
                IsLeasing = viewModel.IsLeasing,
                IsEnabled = viewModel.IsEnabled,
                OtherData = viewModel.OtherData
            };
            
            await _service.CreateCustomerAsync(customer);
            return RedirectToAction(nameof(Index));
        }
        
        // IMPORTANTE: Si hay errores de validación, repoblar los dropdowns
        viewModel.Actors = await _context.Actors
            .Where(a => a.IsEnabled == true)
            .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.FirstFirstName + " " + a.LastFirstName })
            .ToListAsync();
            
        viewModel.CustomerPublicStatusTypes = await _context.CustomerPublicStatusTypes
            .Where(c => c.IsEnabled == true)
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToListAsync();
        
        return View(viewModel);
    }
}
```

### 3. Actualizar Vista

**Ubicación:** `/Views/Customers/Create.cshtml`

```cshtml
@model ari2._0.ViewModels.CustomerCreateViewModel
@{
    ViewData["Title"] = "Nuevo Cliente";
    Layout = "_LayoutDashboard";
}
@section Styles {
    <link rel="stylesheet" href="~/css/views/form-views.css" asp-append-version="true" />
}

<form asp-action="Create" method="post">
    <div asp-validation-summary="ModelOnly" class="alert alert-danger" role="alert"></div>
    <div class="form-container">
        <div class="form-section">
            <h6 class="form-section-title"><i class="bi bi-person-badge"></i>Información del Cliente</h6>
            <div class="row g-3">
                <div class="col-md-6">
                    <label asp-for="ActorsId" class="form-label">Actor</label>
                    <select asp-for="ActorsId" asp-items="Model.Actors" class="form-select">
                        <option value="">Seleccione...</option>
                    </select>
                    <span asp-validation-for="ActorsId" class="text-danger"></span>
                </div>
                <div class="col-md-6">
                    <label asp-for="CustomerPublicStatusTypesId" class="form-label">Estado Público</label>
                    <select asp-for="CustomerPublicStatusTypesId" asp-items="Model.CustomerPublicStatusTypes" class="form-select">
                        <option value="">Seleccione...</option>
                    </select>
                    <span asp-validation-for="CustomerPublicStatusTypesId" class="text-danger"></span>
                </div>
            </div>
        </div>
    </div>
</form>
```

---

## 🔑 Puntos Clave del Patrón

### 1. **Inyección de ApplicationDbContext**

```csharp
public CustomersController(ICustomerService service, ApplicationDbContext context)
{
    _service = service;
    _context = context;  // ← Necesario para consultar catálogos
}
```

### 2. **Filtrado de Registros Activos**

```csharp
.Where(a => a.IsEnabled == true)  // ← Solo mostrar registros activos
```

**Beneficio:** Mejor UX, evita mostrar opciones obsoletas o inactivas.

### 3. **Uso de asp-items en la Vista**

```cshtml
<select asp-for="ActorsId" asp-items="Model.Actors" class="form-select">
    <option value="">Seleccione...</option>
</select>
```

**Nota:** El `<option value="">Seleccione...</option>` es opcional pero recomendado para campos no requeridos.

### 4. **Repoblación en POST con Errores**

```csharp
if (ModelState.IsValid)
{
    // Guardar...
}

// CRÍTICO: Repoblar dropdowns si hay errores de validación
viewModel.Actors = await _context.Actors...
return View(viewModel);
```

**Razón:** Si el formulario tiene errores de validación, los dropdowns deben repoblarse para que el usuario pueda corregir y reenviar.

---

## 📊 Ventajas del Patrón ViewModel

| Característica | ViewBag/ViewData | ViewModel |
|----------------|------------------|-----------|
| **Type-safety** | ❌ No | ✅ Sí |
| **IntelliSense** | ❌ No | ✅ Sí |
| **Compilación** | ❌ Errores en runtime | ✅ Errores en compile-time |
| **Refactoring** | ❌ Propenso a errores | ✅ Seguro |
| **Testabilidad** | ❌ Difícil | ✅ Fácil |
| **Mantenibilidad** | ❌ Baja | ✅ Alta |
| **Recomendado por Microsoft** | ❌ No | ✅ Sí |

---

## 🚀 Próximos Pasos

### Módulos Pendientes de Implementación

1. **Phones** - PhoneCreateViewModel
2. **Emails** - EmailCreateViewModel
3. **Addresses** - AddressCreateViewModel
4. **IdentityCards** - IdentityCardCreateViewModel
5. **ActorRelationships** - ActorRelationshipCreateViewModel
6. **SocialNetworks** - SocialNetworkCreateViewModel

### Patrón a Seguir

Para cada módulo:

1. ✅ Crear `{Module}CreateViewModel.cs` en `/ViewModels/`
2. ✅ Actualizar `{Module}Controller.cs` con inyección de `ApplicationDbContext`
3. ✅ Implementar método `Create()` GET con población de dropdowns
4. ✅ Implementar método `Create()` POST con repoblación en errores
5. ✅ Actualizar vista `Create.cshtml` con `asp-items="Model.{Property}"`

---

## 📚 Referencias

- [Microsoft Learn: Tag Helpers in forms](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-10.0#the-select-tag-helper)
- [Microsoft Learn: Model validation](https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-10.0)
- [Microsoft Learn: Views in ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview?view=aspnetcore-10.0)

---

## ✅ Estado de Implementación

- [x] **Customers** - Implementado y documentado
- [ ] Phones
- [ ] Emails
- [ ] Addresses
- [ ] IdentityCards
- [ ] ActorRelationships
- [ ] SocialNetworks

---

**Fecha de Creación:** 2025-12-03  
**Última Actualización:** 2025-12-03  
**Autor:** Sistema de Desarrollo ARI 2.0
