#!/usr/bin/env python3
import re
from pathlib import Path

CONTROLLERS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Controllers")
VIEWS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Views")

# Mapeo de entidades
ENTITIES = [
    "Actors", "ActorRelationships", "ActorTypes", "Addresses", "AddressTypes",
    "Cities", "Countries", "CustomerPublicStatusTypes", "Customers", "Emails",
    "Genders", "IdentityCards", "IdentityCardTypes", "Municipalities",
    "Neighborhoods", "Phones", "PhoneTypes", "RelationshipTypes",
    "SocialNetworks", "States", "ZipCodes"
]

DELETE_METHOD_SIMPLE = r'(\s+)\[HttpPost, ActionName\("Delete"\)\]\s+\[ValidateAntiForgeryToken\]\s+public async Task<IActionResult> DeleteConfirmed\(Guid id\)\s+\{\s+await _service\.Delete\w+Async\(id\);\s+return RedirectToAction\(nameof\(Index\)\);\s+\}'

DELETE_METHOD_WITH_ERROR = '''$1[HttpPost, ActionName("Delete")]
$1[ValidateAntiForgeryToken]
$1public async Task<IActionResult> DeleteConfirmed(Guid id)
$1{
$1    try
$1    {
$1        await _service.Delete{entity}Async(id);
$1        TempData["SuccessMessage"] = "{display_name} eliminado exitosamente.";
$1        return RedirectToAction(nameof(Index));
$1    }
$1    catch (Microsoft.EntityFrameworkCore.DbUpdateException ex) 
$1        when (ex.InnerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23503")
$1    {
$1        TempData["ErrorMessage"] = "No se puede eliminar este registro porque tiene datos relacionados. Primero debe eliminar o reasignar los registros relacionados.";
$1        return RedirectToAction(nameof(Index));
$1    }
$1    catch (Exception ex)
$1    {
$1        TempData["ErrorMessage"] = $"Error al eliminar: {{ex.Message}}";
$1        return RedirectToAction(nameof(Index));
$1    }
$1}'''

def get_entity_display_name(entity):
    names = {
        "Actors": "Actor", "ActorRelationships": "Relación", "ActorTypes": "Tipo de Actor",
        "Addresses": "Dirección", "AddressTypes": "Tipo de Dirección", "Cities": "Ciudad",
        "Countries": "País", "CustomerPublicStatusTypes": "Estado de Cliente", "Customers": "Cliente",
        "Emails": "Correo", "Genders": "Género", "IdentityCards": "Documento",
        "IdentityCardTypes": "Tipo de Documento", "Municipalities": "Municipio",
        "Neighborhoods": "Barrio", "Phones": "Teléfono", "PhoneTypes": "Tipo de Teléfono",
        "RelationshipTypes": "Tipo de Relación", "SocialNetworks": "Red Social",
        "States": "Estado", "ZipCodes": "Código Postal"
    }
    return names.get(entity, entity)

def update_controller(entity):
    controller_file = CONTROLLERS_DIR / f"{entity}Controller.cs"
    
    if not controller_file.exists():
        print(f"⚠ No encontrado: {controller_file}")
        return False
    
    content = controller_file.read_text(encoding='utf-8')
    
    # Verificar si ya tiene manejo de errores
    if 'DbUpdateException' in content and 'TempData["ErrorMessage"]' in content:
        print(f"✓ {entity}: Ya tiene manejo de errores")
        return True
    
    # Backup
    backup = controller_file.with_suffix('.cs.bak')
    backup.write_text(content, encoding='utf-8')
    
    # Reemplazar método Delete
    entity_singular = entity.rstrip('s') if entity.endswith('s') and entity != "Addresses" else entity
    display_name = get_entity_display_name(entity)
    
    replacement = DELETE_METHOD_WITH_ERROR.replace('{entity}', entity_singular).replace('{display_name}', display_name)
    new_content = re.sub(DELETE_METHOD_SIMPLE, replacement, content, flags=re.DOTALL)
    
    if new_content != content:
        controller_file.write_text(new_content, encoding='utf-8')
        print(f"✓ {entity}: Controller actualizado")
        return True
    else:
        print(f"⚠ {entity}: No se encontró patrón de Delete")
        return False

def update_view(entity):
    index_file = VIEWS_DIR / entity / "Index.cshtml"
    
    if not index_file.exists():
        print(f"⚠ No encontrado: {index_file}")
        return False
    
    content = index_file.read_text(encoding='utf-8')
    
    # Verificar si ya tiene alertas
    if '_AlertMessages' in content:
        print(f"✓ {entity}: Vista ya tiene alertas")
        return True
    
    # Buscar @section Styles y agregar alertas después
    pattern = r'(@section Styles \{[^}]+\})'
    replacement = r'\1\n\n@await Html.PartialAsync("_AlertMessages")'
    
    new_content = re.sub(pattern, replacement, content)
    
    if new_content != content:
        index_file.write_text(new_content, encoding='utf-8')
        print(f"✓ {entity}: Vista actualizada")
        return True
    else:
        print(f"⚠ {entity}: No se pudo actualizar vista")
        return False

def main():
    print("=== Agregando Manejo de Errores ===\n")
    
    print("--- Actualizando Controladores ---")
    for entity in ENTITIES:
        update_controller(entity)
    
    print("\n--- Actualizando Vistas ---")
    for entity in ENTITIES:
        update_view(entity)
    
    print("\n=== Completado ===")

if __name__ == "__main__":
    main()
