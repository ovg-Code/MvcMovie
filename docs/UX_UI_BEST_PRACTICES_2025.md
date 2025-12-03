# Mejores Prácticas de UX/UI 2025 para .NET MVC

**Fecha:** Diciembre 2025  
**Proyecto:** ARI 2.0  
**Framework:** ASP.NET Core MVC 8.0

---

## 📋 Tabla de Contenidos

1. [Principios Fundamentales](#principios-fundamentales)
2. [Arquitectura de Vistas](#arquitectura-de-vistas)
3. [Frameworks CSS Modernos](#frameworks-css-modernos)
4. [Diseño Responsivo](#diseño-responsivo)
5. [Accesibilidad (WCAG 2.1)](#accesibilidad)
6. [Componentes Reutilizables](#componentes-reutilizables)
7. [Performance y Optimización](#performance-y-optimización)
8. [Patrones de Diseño UI](#patrones-de-diseño-ui)
9. [Herramientas y Recursos](#herramientas-y-recursos)

---

## 🎯 Principios Fundamentales

### Separation of Concerns (SoC)

Las vistas en ASP.NET Core MVC deben mantener una clara separación de responsabilidades:

```
┌─────────────────────────────────┐
│  Controller (Lógica de flujo)   │
├─────────────────────────────────┤
│  ViewModel (Datos para vista)   │
├─────────────────────────────────┤
│  View (Presentación HTML)       │
├─────────────────────────────────┤
│  CSS (Estilos)                  │
├─────────────────────────────────┤
│  JavaScript (Comportamiento)    │
└─────────────────────────────────┘
```

**Beneficios:**
- ✅ Mantenibilidad mejorada
- ✅ Testing más fácil
- ✅ Reutilización de código
- ✅ Desarrollo paralelo de equipos

### ViewModels vs Models

**❌ NO HACER:**
```csharp
// Pasar entidades de dominio directamente a las vistas
public IActionResult Index()
{
    var customers = _context.Customers.ToList();
    return View(customers); // ❌ Expone toda la entidad
}
```

**✅ HACER:**
```csharp
// Usar ViewModels específicos para cada vista
public class CustomerListViewModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public IActionResult Index()
{
    var viewModel = _service.GetAllCustomers()
        .Select(c => new CustomerListViewModel
        {
            Id = c.Id,
            FullName = $"{c.FirstName} {c.LastName}",
            Email = c.Email,
            Status = c.IsEnabled ? "Activo" : "Inactivo",
            CreatedAt = c.CreatedAt
        });
    
    return View(viewModel);
}
```

**Ventajas de ViewModels:**
- 🔒 Seguridad: No expones propiedades sensibles
- 📊 Datos específicos: Solo lo que la vista necesita
- 🎨 Formato: Datos pre-formateados para presentación
- ✅ Validación: Reglas específicas por vista

---

## 🏗️ Arquitectura de Vistas

### Estructura de Carpetas Recomendada

```
Views/
├── Shared/
│   ├── _Layout.cshtml           # Layout principal
│   ├── _LayoutAdmin.cshtml      # Layout para admin
│   ├── _LoginPartial.cshtml     # Partial de login
│   ├── _ValidationScripts.cshtml
│   ├── Components/              # View Components
│   │   ├── Navigation/
│   │   ├── Breadcrumb/
│   │   └── UserMenu/
│   └── Partials/                # Partial Views
│       ├── _Pagination.cshtml
│       ├── _SearchBox.cshtml
│       └── _StatusBadge.cshtml
├── Customers/
│   ├── Index.cshtml
│   ├── Details.cshtml
│   ├── Create.cshtml
│   ├── Edit.cshtml
│   └── _CustomerCard.cshtml     # Partial específico
├── _ViewImports.cshtml
└── _ViewStart.cshtml
```

### Layout Principal (_Layout.cshtml)

```html
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - ARI 2.0</title>
    
    <!-- Preconnect para performance -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://cdn.jsdelivr.net">
    
    <!-- CSS Framework (Bootstrap 5 o Tailwind) -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" 
          rel="stylesheet" 
          integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" 
          crossorigin="anonymous">
    
    <!-- Custom CSS -->
    <link rel="stylesheet" href="~/css/site.css" asp-append-version="true" />
    
    @await RenderSectionAsync("Styles", required: false)
</head>
<body>
    <!-- Header con navegación -->
    <header>
        <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
            <div class="container-fluid">
                <a class="navbar-brand" asp-controller="Home" asp-action="Index">
                    ARI 2.0
                </a>
                <button class="navbar-toggler" type="button" 
                        data-bs-toggle="collapse" 
                        data-bs-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav me-auto">
                        <li class="nav-item">
                            <a class="nav-link" asp-controller="Customers" asp-action="Index">
                                Clientes
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" asp-controller="Actors" asp-action="Index">
                                Actores
                            </a>
                        </li>
                    </ul>
                    <partial name="_LoginPartial" />
                </div>
            </div>
        </nav>
    </header>

    <!-- Breadcrumb -->
    <div class="container-fluid mt-3">
        @await Component.InvokeAsync("Breadcrumb")
    </div>

    <!-- Contenido principal -->
    <main role="main" class="container-fluid py-4">
        @RenderBody()
    </main>

    <!-- Footer -->
    <footer class="footer mt-auto py-3 bg-light">
        <div class="container text-center">
            <span class="text-muted">© 2025 ARI 2.0 - Todos los derechos reservados</span>
        </div>
    </footer>

    <!-- Scripts -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"
            integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL"
            crossorigin="anonymous"></script>
    
    <script src="~/js/site.js" asp-append-version="true"></script>
    
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

### Partial Views

**Cuándo usar Partial Views:**
- ✅ Contenido reutilizable sin lógica compleja
- ✅ Reducir duplicación de markup
- ✅ Componentes simples (tarjetas, badges, etc.)

**Ejemplo: _StatusBadge.cshtml**
```html
@model string

@{
    var badgeClass = Model switch
    {
        "Activo" => "bg-success",
        "Inactivo" => "bg-secondary",
        "Pendiente" => "bg-warning",
        _ => "bg-info"
    };
}

<span class="badge @badgeClass">@Model</span>
```

**Uso:**
```html
<partial name="_StatusBadge" model="@customer.Status" />
```

### View Components

**Cuándo usar View Components:**
- ✅ Lógica de renderizado compleja
- ✅ Requiere acceso a base de datos
- ✅ Componentes con estado
- ✅ Widgets reutilizables (carrito, notificaciones, etc.)

**Ejemplo: NavigationViewComponent.cs**
```csharp
public class NavigationViewComponent : ViewComponent
{
    private readonly IMenuService _menuService;

    public NavigationViewComponent(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var menuItems = await _menuService.GetMenuItemsForUserAsync(User);
        return View(menuItems);
    }
}
```

**Vista: Components/Navigation/Default.cshtml**
```html
@model IEnumerable<MenuItem>

<ul class="navbar-nav">
    @foreach (var item in Model)
    {
        <li class="nav-item">
            <a class="nav-link" href="@item.Url">
                <i class="@item.Icon"></i> @item.Title
            </a>
        </li>
    }
</ul>
```

**Uso:**
```html
@await Component.InvokeAsync("Navigation")
```

---

## 🎨 Frameworks CSS Modernos

### Bootstrap 5 (Recomendado para proyectos empresariales)

**Ventajas:**
- ✅ Ecosistema maduro y estable
- ✅ Amplia documentación
- ✅ Componentes empresariales listos
- ✅ Compatibilidad con navegadores antiguos
- ✅ Gran comunidad

**Instalación:**
```html
<!-- CDN -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" 
      rel="stylesheet">
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
```

**Componentes clave para aplicaciones empresariales:**

1. **Cards para listados:**
```html
<div class="card">
    <div class="card-header">
        <h5 class="card-title mb-0">@customer.FullName</h5>
    </div>
    <div class="card-body">
        <p class="card-text">
            <strong>Email:</strong> @customer.Email<br>
            <strong>Teléfono:</strong> @customer.Phone
        </p>
    </div>
    <div class="card-footer">
        <a asp-action="Edit" asp-route-id="@customer.Id" 
           class="btn btn-sm btn-primary">Editar</a>
        <a asp-action="Details" asp-route-id="@customer.Id" 
           class="btn btn-sm btn-info">Ver</a>
    </div>
</div>
```

2. **Tablas responsivas:**
```html
<div class="table-responsive">
    <table class="table table-hover table-striped">
        <thead class="table-dark">
            <tr>
                <th>Nombre</th>
                <th>Email</th>
                <th>Estado</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var customer in Model)
            {
                <tr>
                    <td>@customer.FullName</td>
                    <td>@customer.Email</td>
                    <td><partial name="_StatusBadge" model="@customer.Status" /></td>
                    <td>
                        <div class="btn-group" role="group">
                            <a asp-action="Edit" asp-route-id="@customer.Id" 
                               class="btn btn-sm btn-outline-primary">
                                <i class="bi bi-pencil"></i>
                            </a>
                            <a asp-action="Details" asp-route-id="@customer.Id" 
                               class="btn btn-sm btn-outline-info">
                                <i class="bi bi-eye"></i>
                            </a>
                        </div>
                    </td>
                </tr>
            }
        </tbody>
    </table>
</div>
```

3. **Formularios modernos:**
```html
<form asp-action="Create" method="post" class="needs-validation" novalidate>
    <div class="row g-3">
        <div class="col-md-6">
            <label asp-for="FirstName" class="form-label"></label>
            <input asp-for="FirstName" class="form-control" />
            <span asp-validation-for="FirstName" class="text-danger"></span>
        </div>
        
        <div class="col-md-6">
            <label asp-for="LastName" class="form-label"></label>
            <input asp-for="LastName" class="form-control" />
            <span asp-validation-for="LastName" class="text-danger"></span>
        </div>
        
        <div class="col-12">
            <label asp-for="Email" class="form-label"></label>
            <div class="input-group">
                <span class="input-group-text">@</span>
                <input asp-for="Email" class="form-control" type="email" />
            </div>
            <span asp-validation-for="Email" class="text-danger"></span>
        </div>
        
        <div class="col-12">
            <button type="submit" class="btn btn-primary">
                <i class="bi bi-save"></i> Guardar
            </button>
            <a asp-action="Index" class="btn btn-secondary">
                <i class="bi bi-x-circle"></i> Cancelar
            </a>
        </div>
    </div>
</form>
```

### Tailwind CSS (Alternativa moderna)

**Ventajas:**
- ✅ Utility-first approach
- ✅ Altamente personalizable
- ✅ Tamaño final optimizado
- ✅ Diseño más rápido

**Instalación con CDN (desarrollo):**
```html
<script src="https://cdn.tailwindcss.com"></script>
```

**Ejemplo con Tailwind:**
```html
<div class="bg-white shadow-md rounded-lg p-6">
    <h2 class="text-2xl font-bold text-gray-800 mb-4">@customer.FullName</h2>
    <div class="space-y-2">
        <p class="text-gray-600">
            <span class="font-semibold">Email:</span> @customer.Email
        </p>
        <p class="text-gray-600">
            <span class="font-semibold">Teléfono:</span> @customer.Phone
        </p>
    </div>
    <div class="mt-4 flex gap-2">
        <a asp-action="Edit" asp-route-id="@customer.Id" 
           class="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600">
            Editar
        </a>
        <a asp-action="Details" asp-route-id="@customer.Id" 
           class="px-4 py-2 bg-gray-500 text-white rounded hover:bg-gray-600">
            Ver
        </a>
    </div>
</div>
```

### Comparación Bootstrap vs Tailwind

| Aspecto | Bootstrap 5 | Tailwind CSS |
|---------|-------------|--------------|
| Curva de aprendizaje | Baja | Media |
| Componentes listos | ✅ Muchos | ❌ Pocos |
| Personalización | Media | ✅ Alta |
| Tamaño final | ~200KB | ~10KB (optimizado) |
| Consistencia | ✅ Alta | Requiere disciplina |
| Mejor para | Proyectos empresariales | Diseños únicos |

**Recomendación para ARI 2.0:** Bootstrap 5
- Proyecto empresarial con múltiples desarrolladores
- Necesidad de componentes consistentes
- Tiempo de desarrollo limitado
- Equipo familiarizado con Bootstrap

