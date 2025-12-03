# Mejores Prácticas UX/UI 2025 - Parte 2

## 📱 Diseño Responsivo

### Mobile-First Approach

**Principio:** Diseñar primero para móviles, luego escalar a pantallas más grandes.

```css
/* Base: Mobile (< 576px) */
.customer-card {
    padding: 1rem;
    margin-bottom: 1rem;
}

/* Tablet (≥ 576px) */
@media (min-width: 576px) {
    .customer-card {
        padding: 1.5rem;
    }
}

/* Desktop (≥ 992px) */
@media (min-width: 992px) {
    .customer-card {
        padding: 2rem;
        display: grid;
        grid-template-columns: 1fr 2fr;
    }
}
```

### Breakpoints Estándar 2025

```scss
// Bootstrap 5 breakpoints
$breakpoints: (
    xs: 0,      // Extra small (móviles)
    sm: 576px,  // Small (móviles grandes)
    md: 768px,  // Medium (tablets)
    lg: 992px,  // Large (laptops)
    xl: 1200px, // Extra large (desktops)
    xxl: 1400px // Extra extra large (pantallas grandes)
);
```

### Grid System Responsivo

**Bootstrap Grid:**
```html
<div class="container">
    <div class="row">
        <!-- En móvil: 100%, Tablet: 50%, Desktop: 33.33% -->
        <div class="col-12 col-md-6 col-lg-4">
            <div class="card">
                <!-- Contenido -->
            </div>
        </div>
        <div class="col-12 col-md-6 col-lg-4">
            <div class="card">
                <!-- Contenido -->
            </div>
        </div>
        <div class="col-12 col-md-6 col-lg-4">
            <div class="card">
                <!-- Contenido -->
            </div>
        </div>
    </div>
</div>
```

### Navegación Responsiva

```html
<!-- Navbar que colapsa en móvil -->
<nav class="navbar navbar-expand-lg navbar-dark bg-primary">
    <div class="container-fluid">
        <a class="navbar-brand" href="#">ARI 2.0</a>
        
        <!-- Botón hamburguesa para móvil -->
        <button class="navbar-toggler" type="button" 
                data-bs-toggle="collapse" 
                data-bs-target="#navbarNav"
                aria-controls="navbarNav" 
                aria-expanded="false" 
                aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        
        <!-- Menú colapsable -->
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav ms-auto">
                <li class="nav-item">
                    <a class="nav-link" href="#">Clientes</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Actores</a>
                </li>
            </ul>
        </div>
    </div>
</nav>
```

### Tablas Responsivas

**Opción 1: Scroll horizontal**
```html
<div class="table-responsive">
    <table class="table">
        <!-- Contenido de tabla -->
    </table>
</div>
```

**Opción 2: Cards en móvil, tabla en desktop**
```html
<!-- Vista móvil: Cards -->
<div class="d-lg-none">
    @foreach (var customer in Model)
    {
        <div class="card mb-3">
            <div class="card-body">
                <h5 class="card-title">@customer.FullName</h5>
                <p class="card-text">
                    <strong>Email:</strong> @customer.Email<br>
                    <strong>Teléfono:</strong> @customer.Phone
                </p>
                <a asp-action="Edit" asp-route-id="@customer.Id" 
                   class="btn btn-sm btn-primary">Editar</a>
            </div>
        </div>
    }
</div>

<!-- Vista desktop: Tabla -->
<div class="d-none d-lg-block">
    <table class="table">
        <thead>
            <tr>
                <th>Nombre</th>
                <th>Email</th>
                <th>Teléfono</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var customer in Model)
            {
                <tr>
                    <td>@customer.FullName</td>
                    <td>@customer.Email</td>
                    <td>@customer.Phone</td>
                    <td>
                        <a asp-action="Edit" asp-route-id="@customer.Id" 
                           class="btn btn-sm btn-primary">Editar</a>
                    </td>
                </tr>
            }
        </tbody>
    </table>
</div>
```

### Imágenes Responsivas

```html
<!-- Imagen que se adapta al contenedor -->
<img src="~/images/logo.png" 
     class="img-fluid" 
     alt="Logo ARI 2.0"
     loading="lazy">

<!-- Diferentes imágenes según tamaño de pantalla -->
<picture>
    <source media="(min-width: 1200px)" srcset="~/images/hero-large.jpg">
    <source media="(min-width: 768px)" srcset="~/images/hero-medium.jpg">
    <img src="~/images/hero-small.jpg" alt="Hero" class="img-fluid">
</picture>
```

---

## ♿ Accesibilidad (WCAG 2.1 Nivel AA)

### Principios POUR

1. **Perceptible:** La información debe ser presentada de forma que los usuarios puedan percibirla
2. **Operable:** Los componentes de la interfaz deben ser operables
3. **Comprensible:** La información y operación de la interfaz deben ser comprensibles
4. **Robusto:** El contenido debe ser robusto para ser interpretado por tecnologías asistivas

### Contraste de Colores

**Requisitos WCAG 2.1:**
- Texto normal: Ratio mínimo 4.5:1
- Texto grande (18pt+): Ratio mínimo 3:1
- Componentes UI: Ratio mínimo 3:1

```css
/* ✅ Buen contraste */
.btn-primary {
    background-color: #0056b3; /* Azul oscuro */
    color: #ffffff; /* Blanco */
    /* Ratio: 8.59:1 */
}

/* ❌ Mal contraste */
.btn-light {
    background-color: #f8f9fa; /* Gris muy claro */
    color: #6c757d; /* Gris medio */
    /* Ratio: 2.5:1 - NO CUMPLE */
}
```

**Herramientas para verificar contraste:**
- [WebAIM Contrast Checker](https://webaim.org/resources/contrastchecker/)
- [Contrast Ratio](https://contrast-ratio.com/)

### Etiquetas Semánticas

```html
<!-- ✅ HTML Semántico -->
<header>
    <nav aria-label="Navegación principal">
        <ul>
            <li><a href="#">Inicio</a></li>
            <li><a href="#">Clientes</a></li>
        </ul>
    </nav>
</header>

<main>
    <article>
        <h1>Título Principal</h1>
        <section>
            <h2>Sección 1</h2>
            <p>Contenido...</p>
        </section>
    </article>
</main>

<footer>
    <p>&copy; 2025 ARI 2.0</p>
</footer>

<!-- ❌ NO semántico -->
<div class="header">
    <div class="nav">
        <div class="menu-item">Inicio</div>
    </div>
</div>
```

### ARIA Labels

```html
<!-- Botones con iconos -->
<button type="button" 
        class="btn btn-primary" 
        aria-label="Editar cliente">
    <i class="bi bi-pencil" aria-hidden="true"></i>
</button>

<!-- Links con contexto -->
<a href="#" aria-label="Ver detalles de Juan Pérez">
    Ver más
</a>

<!-- Formularios -->
<label for="email">Correo Electrónico</label>
<input type="email" 
       id="email" 
       name="email"
       aria-required="true"
       aria-describedby="email-help">
<small id="email-help" class="form-text">
    Ingrese un correo válido
</small>

<!-- Estados dinámicos -->
<div role="alert" 
     aria-live="polite" 
     aria-atomic="true">
    Cliente guardado exitosamente
</div>
```

### Navegación por Teclado

```html
<!-- Orden de tabulación lógico -->
<form>
    <input type="text" tabindex="1" placeholder="Nombre">
    <input type="email" tabindex="2" placeholder="Email">
    <input type="tel" tabindex="3" placeholder="Teléfono">
    <button type="submit" tabindex="4">Enviar</button>
</form>

<!-- Skip navigation link -->
<a href="#main-content" class="skip-link">
    Saltar al contenido principal
</a>

<style>
.skip-link {
    position: absolute;
    top: -40px;
    left: 0;
    background: #000;
    color: #fff;
    padding: 8px;
    z-index: 100;
}

.skip-link:focus {
    top: 0;
}
</style>
```

### Formularios Accesibles

```html
<form asp-action="Create" method="post">
    <!-- Agrupación de campos relacionados -->
    <fieldset>
        <legend>Información Personal</legend>
        
        <div class="mb-3">
            <label for="firstName" class="form-label">
                Nombre <span class="text-danger" aria-label="requerido">*</span>
            </label>
            <input type="text" 
                   class="form-control" 
                   id="firstName"
                   name="firstName"
                   required
                   aria-required="true"
                   aria-describedby="firstName-error">
            <span id="firstName-error" 
                  class="text-danger" 
                  role="alert"
                  asp-validation-for="FirstName"></span>
        </div>
        
        <div class="mb-3">
            <label for="email" class="form-label">
                Email <span class="text-danger" aria-label="requerido">*</span>
            </label>
            <input type="email" 
                   class="form-control" 
                   id="email"
                   name="email"
                   required
                   aria-required="true"
                   aria-describedby="email-help email-error">
            <small id="email-help" class="form-text text-muted">
                Nunca compartiremos tu email
            </small>
            <span id="email-error" 
                  class="text-danger" 
                  role="alert"
                  asp-validation-for="Email"></span>
        </div>
    </fieldset>
    
    <button type="submit" class="btn btn-primary">
        Guardar
    </button>
</form>
```

### Mensajes de Error Accesibles

```html
<!-- Mensaje de error global -->
<div class="alert alert-danger" 
     role="alert" 
     aria-live="assertive"
     asp-validation-summary="All">
    <strong>Error:</strong> Por favor corrija los siguientes errores:
</div>

<!-- Validación en tiempo real -->
<script>
document.getElementById('email').addEventListener('blur', function() {
    const email = this.value;
    const errorSpan = document.getElementById('email-error');
    
    if (!isValidEmail(email)) {
        this.setAttribute('aria-invalid', 'true');
        errorSpan.textContent = 'Email inválido';
        errorSpan.setAttribute('role', 'alert');
    } else {
        this.setAttribute('aria-invalid', 'false');
        errorSpan.textContent = '';
    }
});
</script>
```

---

## 🧩 Componentes Reutilizables

### Tag Helpers Personalizados

**StatusBadgeTagHelper.cs:**
```csharp
[HtmlTargetElement("status-badge")]
public class StatusBadgeTagHelper : TagHelper
{
    public string Status { get; set; }
    
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.Attributes.SetAttribute("class", $"badge bg-{GetBadgeColor(Status)}");
        output.Content.SetContent(Status);
    }
    
    private string GetBadgeColor(string status)
    {
        return status?.ToLower() switch
        {
            "activo" => "success",
            "inactivo" => "secondary",
            "pendiente" => "warning",
            "bloqueado" => "danger",
            _ => "info"
        };
    }
}
```

**Uso:**
```html
<status-badge status="@customer.Status"></status-badge>
```

### Partial View con Modelo Tipado

**_CustomerCard.cshtml:**
```html
@model CustomerCardViewModel

<div class="card h-100">
    <div class="card-header bg-primary text-white">
        <h5 class="card-title mb-0">@Model.FullName</h5>
    </div>
    <div class="card-body">
        <dl class="row mb-0">
            <dt class="col-sm-4">Email:</dt>
            <dd class="col-sm-8">@Model.Email</dd>
            
            <dt class="col-sm-4">Teléfono:</dt>
            <dd class="col-sm-8">@Model.Phone</dd>
            
            <dt class="col-sm-4">Estado:</dt>
            <dd class="col-sm-8">
                <status-badge status="@Model.Status"></status-badge>
            </dd>
        </dl>
    </div>
    <div class="card-footer">
        <div class="btn-group w-100" role="group">
            <a asp-action="Edit" 
               asp-route-id="@Model.Id" 
               class="btn btn-sm btn-outline-primary">
                <i class="bi bi-pencil"></i> Editar
            </a>
            <a asp-action="Details" 
               asp-route-id="@Model.Id" 
               class="btn btn-sm btn-outline-info">
                <i class="bi bi-eye"></i> Ver
            </a>
            <a asp-action="Delete" 
               asp-route-id="@Model.Id" 
               class="btn btn-sm btn-outline-danger">
                <i class="bi bi-trash"></i> Eliminar
            </a>
        </div>
    </div>
</div>
```

**Uso:**
```html
@foreach (var customer in Model)
{
    <div class="col-md-6 col-lg-4 mb-3">
        <partial name="_CustomerCard" model="@customer" />
    </div>
}
```

### View Component Complejo

**PaginationViewComponent.cs:**
```csharp
public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(int currentPage, int totalPages, string action)
    {
        var model = new PaginationViewModel
        {
            CurrentPage = currentPage,
            TotalPages = totalPages,
            Action = action
        };
        
        return View(model);
    }
}
```

**Components/Pagination/Default.cshtml:**
```html
@model PaginationViewModel

@if (Model.TotalPages > 1)
{
    <nav aria-label="Navegación de páginas">
        <ul class="pagination justify-content-center">
            <!-- Primera página -->
            <li class="page-item @(Model.CurrentPage == 1 ? "disabled" : "")">
                <a class="page-link" 
                   asp-action="@Model.Action" 
                   asp-route-page="1"
                   aria-label="Primera">
                    <span aria-hidden="true">&laquo;&laquo;</span>
                </a>
            </li>
            
            <!-- Página anterior -->
            <li class="page-item @(Model.CurrentPage == 1 ? "disabled" : "")">
                <a class="page-link" 
                   asp-action="@Model.Action" 
                   asp-route-page="@(Model.CurrentPage - 1)"
                   aria-label="Anterior">
                    <span aria-hidden="true">&laquo;</span>
                </a>
            </li>
            
            <!-- Páginas numeradas -->
            @for (int i = Model.StartPage; i <= Model.EndPage; i++)
            {
                <li class="page-item @(i == Model.CurrentPage ? "active" : "")">
                    <a class="page-link" 
                       asp-action="@Model.Action" 
                       asp-route-page="@i">
                        @i
                        @if (i == Model.CurrentPage)
                        {
                            <span class="visually-hidden">(actual)</span>
                        }
                    </a>
                </li>
            }
            
            <!-- Página siguiente -->
            <li class="page-item @(Model.CurrentPage == Model.TotalPages ? "disabled" : "")">
                <a class="page-link" 
                   asp-action="@Model.Action" 
                   asp-route-page="@(Model.CurrentPage + 1)"
                   aria-label="Siguiente">
                    <span aria-hidden="true">&raquo;</span>
                </a>
            </li>
            
            <!-- Última página -->
            <li class="page-item @(Model.CurrentPage == Model.TotalPages ? "disabled" : "")">
                <a class="page-link" 
                   asp-action="@Model.Action" 
                   asp-route-page="@Model.TotalPages"
                   aria-label="Última">
                    <span aria-hidden="true">&raquo;&raquo;</span>
                </a>
            </li>
        </ul>
    </nav>
}
```

**Uso:**
```html
@await Component.InvokeAsync("Pagination", new { 
    currentPage = Model.CurrentPage, 
    totalPages = Model.TotalPages, 
    action = "Index" 
})
```

