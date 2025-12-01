# Scripts de Desarrollo

## Script Actual

### generate_controller.sh
Script para generar controladores usando scaffolding oficial de Microsoft.

**Uso:**
```bash
./scripts/generate_controller.sh ModelName
```

**Ejemplo:**
```bash
./scripts/generate_controller.sh Product
```

Esto generará:
- `Controllers/ProductsController.cs`
- `Views/Products/Index.cshtml`
- `Views/Products/Create.cshtml`
- `Views/Products/Edit.cshtml`
- `Views/Products/Details.cshtml`
- `Views/Products/Delete.cshtml`

**Importante:** Después de ejecutar, documenta el comando en `docs/development/SCAFFOLDING_HISTORY.md`

---

## Scripts Obsoletos

Los archivos `.OLD` son scripts antiguos que usaban `sed` para modificar archivos.
**NO USAR** - pueden romper el código existente.

Se mantienen solo como referencia histórica.
