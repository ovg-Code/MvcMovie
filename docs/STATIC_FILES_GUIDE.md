# Guía de Archivos Estáticos en ASP.NET Core MVC

## 📁 Estructura de wwwroot

```
wwwroot/
├── css/                    # Hojas de estilo
│   └── site.css
├── js/                     # JavaScript
│   └── site.js
├── images/                 # Imágenes
│   ├── logo.png
│   ├── hero/              # Imágenes hero/banner
│   ├── icons/             # Iconos
│   └── uploads/           # Imágenes subidas por usuarios
├── fonts/                  # Fuentes personalizadas
├── lib/                    # Librerías de terceros
└── favicon.ico
```

## 🖼️ Uso de Imágenes

### En Razor Views (.cshtml)

```html
<!-- Ruta relativa desde wwwroot -->
<img src="~/images/logo.png" alt="Logo ARI 2.0" class="img-fluid">

<!-- Con Tag Helper -->
<img asp-append-version="true" 
     src="~/images/logo.png" 
     alt="Logo ARI 2.0" 
     class="img-fluid">

<!-- Lazy loading (mejor práctica) -->
<img src="~/images/hero.jpg" 
     alt="Hero" 
     loading="lazy" 
     class="img-fluid">
```

### Rutas Absolutas vs Relativas

```html
<!-- ✅ Recomendado: Ruta con ~ (tilde) -->
<img src="~/images/logo.png" alt="Logo">

<!-- ✅ También válido: Ruta absoluta -->
<img src="/images/logo.png" alt="Logo">

<!-- ❌ NO recomendado: Ruta relativa sin ~ -->
<img src="images/logo.png" alt="Logo">
```

## 🎨 CSS Background Images

```css
/* En site.css */
.hero-section {
    background-image: url('/images/hero/banner.jpg');
    background-size: cover;
    background-position: center;
}

/* Con múltiples resoluciones */
.hero-section {
    background-image: url('/images/hero/banner-mobile.jpg');
}

@media (min-width: 768px) {
    .hero-section {
        background-image: url('/images/hero/banner-desktop.jpg');
    }
}
```

## 📦 Organización por Tipo

### Imágenes del Sistema
```
wwwroot/images/
├── logo.png              # Logo principal
├── logo-white.png        # Logo para fondos oscuros
├── favicon.ico           # Favicon
└── placeholder.png       # Imagen placeholder
```

### Imágenes de Contenido
```
wwwroot/images/
├── hero/
│   ├── home-banner.jpg
│   └── about-banner.jpg
├── features/
│   ├── feature-1.jpg
│   └── feature-2.jpg
└── team/
    ├── member-1.jpg
    └── member-2.jpg
```

### Iconos
```
wwwroot/images/icons/
├── check.svg
├── error.svg
└── warning.svg
```

## 🔒 Seguridad

### ✅ Buenas Prácticas

1. **Solo archivos públicos en wwwroot**
   - Nunca guardes archivos sensibles
   - No guardes configuraciones
   - No guardes código fuente

2. **Validar uploads de usuarios**
   ```csharp
   // En el controller
   [HttpPost]
   public async Task<IActionResult> Upload(IFormFile file)
   {
       // Validar extensión
       var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
       var extension = Path.GetExtension(file.FileName).ToLower();
       
       if (!allowedExtensions.Contains(extension))
       {
           return BadRequest("Tipo de archivo no permitido");
       }
       
       // Validar tamaño (5MB máximo)
       if (file.Length > 5 * 1024 * 1024)
       {
           return BadRequest("Archivo muy grande");
       }
       
       // Generar nombre único
       var fileName = $"{Guid.NewGuid()}{extension}";
       var path = Path.Combine(_env.WebRootPath, "images", "uploads", fileName);
       
       using (var stream = new FileStream(path, FileMode.Create))
       {
           await file.CopyToAsync(stream);
       }
       
       return Ok(new { fileName });
   }
   ```

3. **Separar uploads de usuarios**
   ```
   wwwroot/images/uploads/  # Solo para archivos subidos
   ```

## ⚡ Performance

### 1. Optimización de Imágenes

```bash
# Antes de subir al proyecto, optimiza las imágenes:
# - Usa formato WebP cuando sea posible
# - Comprime JPG/PNG
# - Usa SVG para iconos
```

### 2. Lazy Loading

```html
<!-- Cargar imágenes solo cuando sean visibles -->
<img src="~/images/photo.jpg" 
     alt="Foto" 
     loading="lazy"
     width="800" 
     height="600">
```

### 3. Responsive Images

```html
<!-- Diferentes tamaños según dispositivo -->
<picture>
    <source media="(min-width: 1200px)" 
            srcset="~/images/hero-large.jpg">
    <source media="(min-width: 768px)" 
            srcset="~/images/hero-medium.jpg">
    <img src="~/images/hero-small.jpg" 
         alt="Hero" 
         class="img-fluid">
</picture>
```

### 4. Cache Busting con asp-append-version

```html
<!-- Agrega hash del archivo para invalidar cache -->
<img src="~/images/logo.png" 
     asp-append-version="true" 
     alt="Logo">

<!-- Genera: /images/logo.png?v=abc123xyz -->
```

## 🎯 Ejemplo Completo: Hero Section

```html
<!-- En la vista -->
<section class="hero-section">
    <div class="container">
        <div class="row align-items-center">
            <div class="col-md-6">
                <h1>Bienvenido a ARI 2.0</h1>
                <p class="lead">Sistema de Gestión de Clientes</p>
            </div>
            <div class="col-md-6">
                <img src="~/images/hero/dashboard-preview.png" 
                     alt="Dashboard Preview" 
                     class="img-fluid rounded shadow"
                     loading="lazy">
            </div>
        </div>
    </div>
</section>
```

```css
/* En site.css */
.hero-section {
    padding: 4rem 0;
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    color: white;
}

.hero-section img {
    max-width: 100%;
    height: auto;
}
```

## 📝 Checklist

- [ ] Crear carpeta `wwwroot/images`
- [ ] Organizar imágenes por categorías
- [ ] Optimizar todas las imágenes antes de subirlas
- [ ] Usar `loading="lazy"` en imágenes below the fold
- [ ] Agregar atributos `width` y `height` para evitar layout shift
- [ ] Usar `asp-append-version="true"` para cache busting
- [ ] Validar uploads de usuarios
- [ ] Usar formatos modernos (WebP, SVG)
- [ ] Implementar responsive images con `<picture>`
- [ ] Nunca guardar archivos sensibles en wwwroot

## 🔗 Referencias

- [Static Files in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files)
- [Image Optimization Best Practices](https://web.dev/fast/#optimize-your-images)
- [Lazy Loading Images](https://web.dev/browser-level-image-lazy-loading/)
