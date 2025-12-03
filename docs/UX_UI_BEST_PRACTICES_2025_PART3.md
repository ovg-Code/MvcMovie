# Mejores Prácticas UX/UI 2025 - Parte 3 (Final)

## ⚡ Performance y Optimización

### Lazy Loading de Imágenes

```html
<!-- Carga diferida nativa del navegador -->
<img src="~/images/customer-photo.jpg" 
     alt="Foto del cliente"
     loading="lazy"
     width="300" 
     height="200">

<!-- Placeholder mientras carga -->
<img src="data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 300 200'%3E%3C/svg%3E"
     data-src="~/images/customer-photo.jpg"
     alt="Foto del cliente"
     class="lazy"
     width="300" 
     height="200">

<script>
// Intersection Observer para lazy loading
document.addEventListener('DOMContentLoaded', function() {
    const lazyImages = document.querySelectorAll('img.lazy');
    
    const imageObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src;
                img.classList.remove('lazy');
                observer.unobserve(img);
            }
        });
    });
    
    lazyImages.forEach(img => imageObserver.observe(img));
});
</script>
```

### Minificación y Bundling

**Program.cs:**
```csharp
var builder = WebApplication.CreateBuilder(args);

// Configurar bundling y minificación
builder.Services.AddWebOptimizer(pipeline =>
{
    // CSS
    pipeline.AddCssBundle("/css/bundle.css", 
        "css/site.css", 
        "css/custom.css");
    
    // JavaScript
    pipeline.AddJavaScriptBundle("/js/bundle.js", 
        "js/site.js", 
        "js/validation.js");
    
    // Minificar imágenes
    pipeline.MinifyJsFiles();
    pipeline.MinifyCssFiles();
});
```

**_Layout.cshtml:**
```html
<head>
    <!-- CSS Bundle -->
    <link rel="stylesheet" href="/css/bundle.css" asp-append-version="true" />
</head>
<body>
    <!-- JavaScript Bundle al final del body -->
    <script src="/js/bundle.js" asp-append-version="true"></script>
</body>
```

### Caché de Recursos Estáticos

**Program.cs:**
```csharp
var app = builder.Build();

// Configurar caché para archivos estáticos
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cachear por 1 año
        ctx.Context.Response.Headers.Append(
            "Cache-Control", 
            "public,max-age=31536000");
    }
});
```

### Preload de Recursos Críticos

```html
<head>
    <!-- Preload de fuentes -->
    <link rel="preload" 
          href="~/fonts/custom-font.woff2" 
          as="font" 
          type="font/woff2" 
          crossorigin>
    
    <!-- Preload de CSS crítico -->
    <link rel="preload" 
          href="~/css/critical.css" 
          as="style">
    
    <!-- Preconnect a CDNs -->
    <link rel="preconnect" href="https://cdn.jsdelivr.net">
    <link rel="dns-prefetch" href="https://cdn.jsdelivr.net">
</head>
```

### Optimización de Consultas

**Controller con paginación:**
```csharp
public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
{
    var totalItems = await _service.GetTotalCustomersAsync();
    var customers = await _service.GetCustomersPagedAsync(page, pageSize);
    
    var viewModel = new CustomerListViewModel
    {
        Customers = customers,
        CurrentPage = page,
        PageSize = pageSize,
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
    };
    
    return View(viewModel);
}
```

**Service con paginación eficiente:**
```csharp
public async Task<IEnumerable<CustomerViewModel>> GetCustomersPagedAsync(
    int page, 
    int pageSize)
{
    return await _repository.GetAllAsync()
        .OrderBy(c => c.CreatedAt)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(c => new CustomerViewModel
        {
            Id = c.Id,
            FullName = $"{c.FirstName} {c.LastName}",
            Email = c.Email,
            Status = c.IsEnabled ? "Activo" : "Inactivo"
        })
        .ToListAsync();
}
```

---

## 🎨 Patrones de Diseño UI

### 1. Dashboard Layout

```html
<div class="container-fluid">
    <div class="row">
        <!-- Sidebar -->
        <nav class="col-md-3 col-lg-2 d-md-block bg-light sidebar">
            <div class="position-sticky pt-3">
                <ul class="nav flex-column">
                    <li class="nav-item">
                        <a class="nav-link active" href="#">
                            <i class="bi bi-house-door"></i> Dashboard
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">
                            <i class="bi bi-people"></i> Clientes
                        </a>
                    </li>
                </ul>
            </div>
        </nav>
        
        <!-- Main content -->
        <main class="col-md-9 ms-sm-auto col-lg-10 px-md-4">
            <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                <h1 class="h2">Dashboard</h1>
            </div>
            
            <!-- Stats Cards -->
            <div class="row g-3 mb-4">
                <div class="col-md-3">
                    <div class="card text-white bg-primary">
                        <div class="card-body">
                            <h5 class="card-title">Clientes</h5>
                            <p class="card-text display-4">1,234</p>
                        </div>
                    </div>
                </div>
                <!-- Más cards... -->
            </div>
            
            <!-- Content -->
            @RenderBody()
        </main>
    </div>
</div>
```

### 2. Master-Detail Pattern

```html
<div class="row">
    <!-- Lista (Master) -->
    <div class="col-md-4">
        <div class="list-group">
            @foreach (var customer in Model.Customers)
            {
                <a href="#" 
                   class="list-group-item list-group-item-action"
                   data-customer-id="@customer.Id"
                   onclick="loadCustomerDetails('@customer.Id')">
                    <div class="d-flex w-100 justify-content-between">
                        <h5 class="mb-1">@customer.FullName</h5>
                        <small>@customer.CreatedAt.ToString("dd/MM/yyyy")</small>
                    </div>
                    <p class="mb-1">@customer.Email</p>
                </a>
            }
        </div>
    </div>
    
    <!-- Detalles (Detail) -->
    <div class="col-md-8">
        <div id="customer-details" class="card">
            <div class="card-body">
                <p class="text-muted">Seleccione un cliente para ver los detalles</p>
            </div>
        </div>
    </div>
</div>

<script>
async function loadCustomerDetails(customerId) {
    const response = await fetch(`/Customers/Details/${customerId}`);
    const html = await response.text();
    document.getElementById('customer-details').innerHTML = html;
}
</script>
```

### 3. Modal para Formularios

```html
<!-- Botón que abre modal -->
<button type="button" 
        class="btn btn-primary" 
        data-bs-toggle="modal" 
        data-bs-target="#createCustomerModal">
    <i class="bi bi-plus-circle"></i> Nuevo Cliente
</button>

<!-- Modal -->
<div class="modal fade" 
     id="createCustomerModal" 
     tabindex="-1" 
     aria-labelledby="createCustomerModalLabel" 
     aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="createCustomerModalLabel">
                    Nuevo Cliente
                </h5>
                <button type="button" 
                        class="btn-close" 
                        data-bs-dismiss="modal" 
                        aria-label="Cerrar"></button>
            </div>
            <form asp-action="Create" method="post">
                <div class="modal-body">
                    <div class="mb-3">
                        <label asp-for="FirstName" class="form-label"></label>
                        <input asp-for="FirstName" class="form-control" />
                        <span asp-validation-for="FirstName" class="text-danger"></span>
                    </div>
                    <!-- Más campos... -->
                </div>
                <div class="modal-footer">
                    <button type="button" 
                            class="btn btn-secondary" 
                            data-bs-dismiss="modal">
                        Cancelar
                    </button>
                    <button type="submit" class="btn btn-primary">
                        Guardar
                    </button>
                </div>
            </form>
        </div>
    </div>
</div>
```

### 4. Búsqueda y Filtros

```html
<div class="card mb-4">
    <div class="card-body">
        <form asp-action="Index" method="get">
            <div class="row g-3">
                <!-- Búsqueda por texto -->
                <div class="col-md-4">
                    <label for="searchTerm" class="form-label">Buscar</label>
                    <div class="input-group">
                        <span class="input-group-text">
                            <i class="bi bi-search"></i>
                        </span>
                        <input type="text" 
                               class="form-control" 
                               id="searchTerm"
                               name="searchTerm"
                               value="@ViewData["SearchTerm"]"
                               placeholder="Nombre, email...">
                    </div>
                </div>
                
                <!-- Filtro por estado -->
                <div class="col-md-3">
                    <label for="status" class="form-label">Estado</label>
                    <select class="form-select" id="status" name="status">
                        <option value="">Todos</option>
                        <option value="active">Activo</option>
                        <option value="inactive">Inactivo</option>
                    </select>
                </div>
                
                <!-- Filtro por fecha -->
                <div class="col-md-3">
                    <label for="dateFrom" class="form-label">Desde</label>
                    <input type="date" 
                           class="form-control" 
                           id="dateFrom"
                           name="dateFrom"
                           value="@ViewData["DateFrom"]">
                </div>
                
                <!-- Botones -->
                <div class="col-md-2 d-flex align-items-end">
                    <button type="submit" class="btn btn-primary w-100">
                        Filtrar
                    </button>
                </div>
            </div>
        </form>
    </div>
</div>
```

### 5. Notificaciones Toast

```html
<!-- Container para toasts -->
<div class="toast-container position-fixed top-0 end-0 p-3">
    <div id="successToast" 
         class="toast align-items-center text-white bg-success border-0" 
         role="alert" 
         aria-live="assertive" 
         aria-atomic="true">
        <div class="d-flex">
            <div class="toast-body">
                <i class="bi bi-check-circle me-2"></i>
                <span id="toastMessage"></span>
            </div>
            <button type="button" 
                    class="btn-close btn-close-white me-2 m-auto" 
                    data-bs-dismiss="toast" 
                    aria-label="Cerrar"></button>
        </div>
    </div>
</div>

<script>
function showToast(message, type = 'success') {
    const toast = document.getElementById('successToast');
    const toastMessage = document.getElementById('toastMessage');
    
    // Cambiar color según tipo
    toast.className = `toast align-items-center text-white bg-${type} border-0`;
    
    // Cambiar icono según tipo
    const icon = type === 'success' ? 'check-circle' : 
                 type === 'danger' ? 'x-circle' : 
                 'info-circle';
    
    toastMessage.innerHTML = `<i class="bi bi-${icon} me-2"></i>${message}`;
    
    // Mostrar toast
    const bsToast = new bootstrap.Toast(toast);
    bsToast.show();
}

// Uso después de guardar
// showToast('Cliente guardado exitosamente', 'success');
</script>
```

---

## 🛠️ Herramientas y Recursos

### Iconos

**Bootstrap Icons (Recomendado):**
```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.2/font/bootstrap-icons.min.css">

<!-- Uso -->
<i class="bi bi-house"></i>
<i class="bi bi-person"></i>
<i class="bi bi-envelope"></i>
```

**Font Awesome (Alternativa):**
```html
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">

<!-- Uso -->
<i class="fas fa-home"></i>
<i class="fas fa-user"></i>
<i class="fas fa-envelope"></i>
```

### Fuentes

**Google Fonts:**
```html
<link rel="preconnect" href="https://fonts.googleapis.com">
<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@300;400;500;600;700&display=swap" rel="stylesheet">

<style>
body {
    font-family: 'Inter', sans-serif;
}
</style>
```

### Validación de Formularios

**jQuery Validation (incluido en ASP.NET Core):**
```html
@section Scripts {
    <partial name="_ValidationScriptsPartial" />
    
    <script>
    $(document).ready(function() {
        // Validación personalizada
        $.validator.addMethod("phoneNumber", function(value, element) {
            return this.optional(element) || /^\d{10}$/.test(value);
        }, "Por favor ingrese un número de teléfono válido (10 dígitos)");
        
        // Aplicar validación
        $("#customerForm").validate({
            rules: {
                phone: {
                    phoneNumber: true
                }
            }
        });
    });
    </script>
}
```

### Datepicker

**Bootstrap Datepicker:**
```html
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.10.0/dist/css/bootstrap-datepicker3.min.css">
<script src="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.10.0/dist/js/bootstrap-datepicker.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap-datepicker@1.10.0/dist/locales/bootstrap-datepicker.es.min.js"></script>

<input type="text" 
       class="form-control datepicker" 
       asp-for="BirthDate"
       placeholder="dd/mm/yyyy">

<script>
$('.datepicker').datepicker({
    format: 'dd/mm/yyyy',
    language: 'es',
    autoclose: true,
    todayHighlight: true
});
</script>
```

### DataTables para Tablas Avanzadas

```html
<link rel="stylesheet" href="https://cdn.datatables.net/1.13.7/css/dataTables.bootstrap5.min.css">
<script src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.min.js"></script>
<script src="https://cdn.datatables.net/1.13.7/js/dataTables.bootstrap5.min.js"></script>

<table id="customersTable" class="table table-striped">
    <thead>
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
                <td>@customer.Status</td>
                <td>
                    <a asp-action="Edit" asp-route-id="@customer.Id" 
                       class="btn btn-sm btn-primary">Editar</a>
                </td>
            </tr>
        }
    </tbody>
</table>

<script>
$(document).ready(function() {
    $('#customersTable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/es-ES.json'
        },
        pageLength: 25,
        order: [[0, 'asc']],
        responsive: true
    });
});
</script>
```

---

## 📋 Checklist de Implementación

### Antes de Empezar
- [ ] Definir paleta de colores (verificar contraste WCAG)
- [ ] Elegir framework CSS (Bootstrap 5 recomendado)
- [ ] Configurar sistema de iconos
- [ ] Definir tipografía
- [ ] Crear guía de estilos

### Durante el Desarrollo
- [ ] Usar ViewModels en lugar de Models
- [ ] Implementar layouts consistentes
- [ ] Crear componentes reutilizables
- [ ] Aplicar diseño responsivo (mobile-first)
- [ ] Agregar ARIA labels
- [ ] Validar contraste de colores
- [ ] Implementar navegación por teclado
- [ ] Optimizar imágenes (lazy loading)
- [ ] Minificar CSS y JavaScript

### Testing
- [ ] Probar en diferentes navegadores
- [ ] Probar en diferentes dispositivos
- [ ] Validar accesibilidad con herramientas
- [ ] Verificar performance (Lighthouse)
- [ ] Probar navegación por teclado
- [ ] Probar con lectores de pantalla

### Herramientas de Testing
- **Lighthouse:** Auditoría de performance y accesibilidad
- **WAVE:** Evaluación de accesibilidad web
- **axe DevTools:** Testing de accesibilidad
- **BrowserStack:** Testing multi-navegador
- **Chrome DevTools:** Responsive design mode

---

## 📚 Referencias y Recursos

### Documentación Oficial
- [ASP.NET Core MVC Views](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview)
- [Bootstrap 5 Documentation](https://getbootstrap.com/docs/5.3/)
- [WCAG 2.1 Guidelines](https://www.w3.org/WAI/WCAG21/quickref/)
- [MDN Web Docs](https://developer.mozilla.org/)

### Herramientas de Diseño
- [Figma](https://www.figma.com/) - Diseño de interfaces
- [Adobe XD](https://www.adobe.com/products/xd.html) - Prototipado
- [Coolors](https://coolors.co/) - Generador de paletas
- [Google Fonts](https://fonts.google.com/) - Fuentes web

### Validadores
- [W3C HTML Validator](https://validator.w3.org/)
- [W3C CSS Validator](https://jigsaw.w3.org/css-validator/)
- [WebAIM Contrast Checker](https://webaim.org/resources/contrastchecker/)

---

## 🎯 Recomendaciones Finales para ARI 2.0

1. **Framework CSS:** Bootstrap 5
   - Maduro y estable
   - Componentes empresariales
   - Equipo familiarizado

2. **Iconos:** Bootstrap Icons
   - Integración perfecta con Bootstrap
   - Ligero y rápido
   - Amplia variedad

3. **Arquitectura de Vistas:**
   - ViewModels para todas las vistas
   - View Components para lógica compleja
   - Partial Views para componentes simples
   - Tag Helpers personalizados para elementos repetitivos

4. **Accesibilidad:**
   - Cumplir WCAG 2.1 Nivel AA mínimo
   - Probar con lectores de pantalla
   - Navegación completa por teclado

5. **Performance:**
   - Lazy loading de imágenes
   - Minificación y bundling
   - Paginación en listados
   - Caché de recursos estáticos

6. **Responsive Design:**
   - Mobile-first approach
   - Breakpoints estándar de Bootstrap
   - Tablas adaptativas (cards en móvil)

---

**Última actualización:** Diciembre 2025  
**Versión:** 1.0  
**Autor:** Equipo ARI 2.0
