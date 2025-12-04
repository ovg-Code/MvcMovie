# Implementación del Modal de Confirmación de Eliminación

## Descripción General

Se ha implementado un modal de confirmación reutilizable para reemplazar las páginas individuales de eliminación (Delete.cshtml) en todas las entidades. Esto mejora la UX al evitar navegación innecesaria y proporciona una confirmación más rápida y moderna.

## Componentes Implementados

### 1. Partial View Compartido
**Ubicación:** `/Views/Shared/_DeleteConfirmationModal.cshtml`

Modal de Bootstrap 5 reutilizable que contiene:
- Título configurable
- Mensaje de confirmación
- Nombre del elemento a eliminar (dinámico)
- Botones de Cancelar y Eliminar
- Formulario con token anti-falsificación

### 2. JavaScript Helper
**Ubicación:** `/wwwroot/js/delete-confirmation.js`

Función `confirmDelete(url, itemName)` que:
- Configura la URL del formulario de eliminación
- Establece el nombre del elemento a mostrar
- Muestra el modal de Bootstrap

### 3. Integración en Layout
**Ubicación:** `/Views/Shared/_Layout.cshtml`

El modal y el script están incluidos globalmente:
```cshtml
@await Html.PartialAsync("_DeleteConfirmationModal")
<script src="~/js/delete-confirmation.js" asp-append-version="true"></script>
```

## Cómo Implementar en una Vista Index

### Paso 1: Modificar el Botón de Eliminación

**Antes (enlace directo):**
```cshtml
<a asp-action="Delete" asp-route-id="@item.Id" class="action-btn delete" title="Eliminar">
    <i class="bi bi-trash"></i>
</a>
```

**Después (botón con modal):**
```cshtml
<button type="button" 
        onclick="confirmDelete('@Url.Action("Delete", new { id = item.Id })', 'Nombre de la Entidad')" 
        class="action-btn delete border-0 bg-transparent" 
        title="Eliminar">
    <i class="bi bi-trash"></i>
</button>
```

### Paso 2: Personalizar el Nombre de la Entidad

Reemplaza `'Nombre de la Entidad'` con el nombre descriptivo de tu entidad:
- `'Cliente'`
- `'Actor'`
- `'Teléfono'`
- `'Dirección'`
- etc.

Opcionalmente, puedes incluir información específica del registro:
```cshtml
onclick="confirmDelete('@Url.Action("Delete", new { id = item.Id })', 'Cliente: @item.ActorsId')"
```

### Paso 3: Verificar el Controlador

Asegúrate de que el controlador tenga el método HttpPost Delete correcto:

```csharp
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(Guid id)
{
    await _service.DeleteEntityAsync(id);
    return RedirectToAction(nameof(Index));
}
```

### Paso 4: Eliminar la Vista Delete.cshtml

Una vez implementado el modal, puedes eliminar el archivo:
```bash
rm Views/EntityName/Delete.cshtml
```

## Ejemplo Completo: Customers

```cshtml
@foreach (var item in Model)
{
    <tr>
        <td>@item.Name</td>
        <td class="text-end">
            <a asp-action="Edit" asp-route-id="@item.Id" class="action-btn" title="Editar">
                <i class="bi bi-pencil"></i>
            </a>
            <button type="button" 
                    onclick="confirmDelete('@Url.Action("Delete", new { id = item.Id })', 'Cliente')" 
                    class="action-btn delete border-0 bg-transparent" 
                    title="Eliminar">
                <i class="bi bi-trash"></i>
            </button>
        </td>
    </tr>
}
```

## Ventajas de Este Enfoque

1. **Mejor UX**: No requiere navegación a otra página
2. **Más rápido**: Confirmación inmediata sin carga de página
3. **Consistente**: Mismo comportamiento en todas las entidades
4. **Menos código**: Elimina 21 vistas Delete.cshtml individuales
5. **Mantenible**: Un solo componente para actualizar
6. **Moderno**: Usa Bootstrap 5 y JavaScript estándar

## Mejores Prácticas

1. **Siempre incluir el nombre de la entidad** en el segundo parámetro de `confirmDelete()`
2. **Mantener el token anti-falsificación** en el formulario del modal
3. **Usar `border-0 bg-transparent`** en el botón para mantener el estilo de los action-btn
4. **Verificar que el método del controlador** tenga `[ValidateAntiForgeryToken]`

## Entidades Pendientes de Migración

- [ ] Actors
- [ ] ActorRelationships
- [ ] Phones
- [ ] Emails
- [ ] Addresses
- [ ] SocialNetworks
- [ ] IdentityCards
- [ ] Countries
- [ ] States
- [ ] Cities
- [ ] Municipalities
- [ ] Neighborhoods
- [ ] ZipCodes
- [ ] ActorTypes
- [ ] AddressTypes
- [ ] PhoneTypes
- [ ] IdentityCardTypes
- [ ] RelationshipTypes
- [ ] CustomerPublicStatusTypes
- [ ] Genders

## Notas Técnicas

- El modal usa Bootstrap 5 nativo (no requiere jQuery)
- El formulario se envía como POST con el token anti-falsificación
- La función JavaScript es global y está disponible en todas las páginas
- El modal se cierra automáticamente al hacer clic en Cancelar o fuera del modal
