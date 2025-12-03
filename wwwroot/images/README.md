# Carpeta de Imágenes - ARI 2.0

## 📁 Estructura Recomendada

```
images/
├── logo.png                    # Logo principal del sistema
├── logo-white.png              # Logo para fondos oscuros
├── favicon.ico                 # Favicon del sitio
├── hero-dashboard.png          # Imagen hero del dashboard
├── hero/                       # Imágenes de banners
├── icons/                      # Iconos personalizados
└── uploads/                    # Imágenes subidas por usuarios
```

## 🖼️ Imagen del Dashboard (Hero)

Para agregar la imagen principal del dashboard:

1. **Nombre del archivo:** `hero-dashboard.png`
2. **Ubicación:** Coloca el archivo aquí: `wwwroot/images/hero-dashboard.png`
3. **Dimensiones recomendadas:** 800x600 px o 1200x900 px
4. **Formato:** PNG o JPG (optimizado)
5. **Tamaño máximo:** 500 KB

### Cómo agregar la imagen:

1. Guarda tu imagen como `hero-dashboard.png`
2. Cópiala a esta carpeta: `/wwwroot/images/`
3. La vista `Home/Index.cshtml` la mostrará automáticamente

### Optimización de Imágenes

Antes de agregar imágenes, optimízalas:

- **Online:** [TinyPNG](https://tinypng.com/), [Squoosh](https://squoosh.app/)
- **Herramientas:** ImageOptim, GIMP, Photoshop
- **Formato WebP:** Considera usar WebP para mejor compresión

## 🎨 Logo del Sistema

Para agregar el logo:

1. **Archivo:** `logo.png` (fondo transparente)
2. **Dimensiones:** 200x50 px (aproximado)
3. **Formato:** PNG con transparencia
4. **Uso:** Se mostrará en el sidebar y navbar

## 📝 Convenciones de Nombres

- Usa minúsculas y guiones: `hero-dashboard.png`
- Sé descriptivo: `customer-avatar-placeholder.png`
- Incluye dimensiones si hay múltiples versiones: `logo-200x50.png`

## 🔒 Seguridad

- ✅ Solo archivos públicos en esta carpeta
- ❌ Nunca guardes archivos sensibles aquí
- ✅ Valida uploads de usuarios
- ✅ Usa carpeta separada para uploads: `images/uploads/`

## 📚 Más Información

Ver documentación completa en: `/docs/STATIC_FILES_GUIDE.md`
