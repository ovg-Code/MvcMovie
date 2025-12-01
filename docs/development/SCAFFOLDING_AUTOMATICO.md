# Scaffolding Automático en ASP.NET Core MVC

## ¿Qué es Scaffolding?

**Scaffolding** es una característica **oficial de Microsoft** que genera automáticamente código CRUD (Create, Read, Update, Delete) para tus modelos de Entity Framework.

**Documentación oficial:**
- [ASP.NET Core Scaffolding](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator)
- [Tutorial oficial de Microsoft](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model)

## ¿Qué genera automáticamente?

Para cada modelo, genera:
1. **1 Controlador** con acciones CRUD completas
2. **5 Vistas Razor**:
   - Index.cshtml (lista)
   - Create.cshtml (crear)
   - Edit.cshtml (editar)
   - Delete.cshtml (eliminar)
   - Details.cshtml (detalles)

## Requisitos Previos

### 1. Paquetes NuGet necesarios

```bash
# Herramienta de scaffolding
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

# Entity Framework Core (versión compatible)
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Provider de base de datos (PostgreSQL en nuestro caso)
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

### 2. Herramienta global

```bash
# Instalar herramienta global de scaffolding
dotnet tool install -g dotnet-aspnet-codegenerator
```

## Comando Básico

```bash
dotnet aspnet-codegenerator controller \
  -name NombreController \
  -m ModeloClase \
  -dc DbContextClase \
  --relativeFolderPath Controllers \
  --useDefaultLayout \
  --referenceScriptLibraries
```

## Parámetros Explicados

| Parámetro | Descripción | Ejemplo |
|-----------|-------------|---------|
| `-name` | Nombre del controlador a generar | `ActorsController` |
| `-m` | Modelo (clase) a usar | `Actor` |
| `-dc` | DbContext a usar | `ApplicationDbContext` |
| `--relativeFolderPath` | Carpeta donde guardar el controlador | `Controllers` |
| `--useDefaultLayout` | Usar layout por defecto | (flag) |
| `--referenceScriptLibraries` | Incluir scripts de validación | (flag) |
| `--databaseProvider` | Provider de BD (importante para PostgreSQL) | `postgres` |

## Ejemplo Real: Generar Controlador Actor

```bash
cd /ruta/al/proyecto

dotnet aspnet-codegenerator controller \
  -name ActorsController \
  -m Actor \
  -dc ApplicationDbContext \
  --relativeFolderPath Controllers \
  --useDefaultLayout \
  --referenceScriptLibraries \
  --databaseProvider postgres
```

**Resultado:**
```
Added Controller : '/Controllers/ActorsController.cs'
Added View : /Views/Actors/Create.cshtml
Added View : /Views/Actors/Edit.cshtml
Added View : /Views/Actors/Details.cshtml
Added View : /Views/Actors/Delete.cshtml
Added View : /Views/Actors/Index.cshtml
```

## Generación en Batch (Múltiples Modelos)

Para generar controladores para múltiples modelos de una vez:

```bash
# Lista de modelos
MODELS="Actor Customer Phone Email Address"

# Loop para generar cada uno
for model in $MODELS; do
  echo "Generando $model..."
  dotnet aspnet-codegenerator controller \
    -name ${model}sController \
    -m $model \
    -dc ApplicationDbContext \
    --relativeFolderPath Controllers \
    --useDefaultLayout \
    --referenceScriptLibraries \
    --databaseProvider postgres
done
```

## Importante para PostgreSQL

⚠️ **CRÍTICO**: Debes especificar `--databaseProvider postgres`

Sin este parámetro, el scaffolder busca SQL Server por defecto y falla:
```
Error: To scaffold, install Microsoft.EntityFrameworkCore.SqlServer
```

Con el parámetro correcto:
```bash
--databaseProvider postgres
```

El scaffolder usa Npgsql correctamente.

## Compatibilidad de Versiones

**Importante**: Las versiones deben ser compatibles:

| EF Core | Scaffolder | Npgsql |
|---------|------------|--------|
| 8.0.x | 8.0.x | 8.0.x |
| 9.0.x | 9.0.x | 9.0.x |

### Nuestro Proyecto (Actualizado)

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0" />
<PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="9.0.0" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.2" />
```

## Código Generado

### Controlador (Ejemplo)

```csharp
public class ActorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ActorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Actors
    public async Task<IActionResult> Index()
    {
        return View(await _context.Actors.ToListAsync());
    }

    // GET: Actors/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null) return NotFound();
        
        var actor = await _context.Actors
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (actor == null) return NotFound();
        
        return View(actor);
    }

    // GET: Actors/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Actors/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,...")] Actor actor)
    {
        if (ModelState.IsValid)
        {
            // UUID generado automáticamente por PostgreSQL (uuid_generate_v4())
            _context.Add(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(actor);
    }

    // ... Edit, Delete, etc.
}
```

### Vista Index (Ejemplo)

```html
@model IEnumerable<Actor>

<h1>Index</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
<table class="table">
    <thead>
        <tr>
            <th>@Html.DisplayNameFor(model => model.Name)</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
@foreach (var item in Model) {
        <tr>
            <td>@Html.DisplayFor(modelItem => item.Name)</td>
            <td>
                <a asp-action="Edit" asp-route-id="@item.Id">Edit</a> |
                <a asp-action="Details" asp-route-id="@item.Id">Details</a> |
                <a asp-action="Delete" asp-route-id="@item.Id">Delete</a>
            </td>
        </tr>
}
    </tbody>
</table>
```

## Ventajas del Scaffolding

1. ✅ **Ahorro de tiempo**: Genera en segundos lo que tomaría horas manualmente
2. ✅ **Código consistente**: Sigue patrones estándar de Microsoft
3. ✅ **Menos errores**: Código probado y validado
4. ✅ **Buenas prácticas**: Incluye validación, async/await, etc.
5. ✅ **Personalizable**: Puedes modificar el código generado

## Desventajas

1. ⚠️ **Código genérico**: Puede necesitar personalización
2. ⚠️ **Sobrescritura**: Regenerar sobrescribe cambios manuales
3. ⚠️ **Dependencias**: Requiere paquetes adicionales

## Alternativas

### 1. Visual Studio (GUI)
1. Click derecho en carpeta Controllers
2. Add > New Scaffolded Item
3. MVC Controller with views, using Entity Framework
4. Seleccionar modelo y DbContext

### 2. Manual
Crear controladores y vistas manualmente (más control, más tiempo).

## Nuestro Uso en ARIV2

Generamos **21 controladores** con **105 vistas** en minutos:

```bash
# 11 tablas nuevas generadas automáticamente
for model in Actor Customer Phone Email Address \
             IdentityCard IdentityCardType ActorRelationship \
             RelationshipType SocialNetwork CustomerPublicStatusType; do
  dotnet aspnet-codegenerator controller \
    -name ${model}sController \
    -m $model \
    -dc ApplicationDbContext \
    --relativeFolderPath Controllers \
    --useDefaultLayout \
    --referenceScriptLibraries \
    --databaseProvider postgres
done
```

**Resultado**: Módulo Cliente completo en ~5 minutos vs ~8 horas manual.

## Referencias

- [Documentación oficial de aspnet-codegenerator](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator)
- [Tutorial: Agregar modelo a app MVC](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model)
- [Scaffolding en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model#scaffold-movie-pages)
- [GitHub: aspnet-codegenerator](https://github.com/dotnet/Scaffolding)

## Conclusión

El scaffolding **NO es un truco**, es una **herramienta oficial de Microsoft** diseñada para acelerar el desarrollo siguiendo las mejores prácticas de ASP.NET Core MVC.

Es ampliamente usado en la industria y recomendado por Microsoft para proyectos empresariales.
