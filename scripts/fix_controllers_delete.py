#!/usr/bin/env python3
import re
from pathlib import Path

CONTROLLERS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Controllers")

ENTITIES = {
    "ActorRelationships": "Relación", "ActorTypes": "Tipo de Actor",
    "Addresses": "Dirección", "AddressTypes": "Tipo de Dirección",
    "Cities": "Ciudad", "Countries": "País",
    "CustomerPublicStatusTypes": "Estado de Cliente", "Emails": "Correo",
    "Genders": "Género", "IdentityCards": "Documento",
    "IdentityCardTypes": "Tipo de Documento", "Municipalities": "Municipio",
    "Neighborhoods": "Barrio", "Phones": "Teléfono",
    "PhoneTypes": "Tipo de Teléfono", "RelationshipTypes": "Tipo de Relación",
    "SocialNetworks": "Red Social", "States": "Estado", "ZipCodes": "Código Postal"
}

OLD_DELETE = '''        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }'''

NEW_DELETE_TEMPLATE = '''        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {{
            try
            {{
                await _service.DeleteAsync(id);
                TempData["SuccessMessage"] = "{display_name} eliminado exitosamente.";
                return RedirectToAction(nameof(Index));
            }}
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex) 
                when (ex.InnerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "23503")
            {{
                TempData["ErrorMessage"] = "No se puede eliminar este registro porque tiene datos relacionados. Primero debe eliminar o reasignar los registros relacionados.";
                return RedirectToAction(nameof(Index));
            }}
            catch (Exception ex)
            {{
                TempData["ErrorMessage"] = $"Error al eliminar: {{ex.Message}}";
                return RedirectToAction(nameof(Index));
            }}
        }}'''

def update_controller(entity, display_name):
    controller_file = CONTROLLERS_DIR / f"{entity}Controller.cs"
    
    if not controller_file.exists():
        return False
    
    content = controller_file.read_text(encoding='utf-8')
    
    if 'DbUpdateException' in content:
        print(f"✓ {entity}: Ya actualizado")
        return True
    
    new_delete = NEW_DELETE_TEMPLATE.format(display_name=display_name)
    new_content = content.replace(OLD_DELETE, new_delete)
    
    if new_content != content:
        controller_file.write_text(new_content, encoding='utf-8')
        print(f"✓ {entity}: Actualizado")
        return True
    else:
        print(f"⚠ {entity}: No se pudo actualizar")
        return False

def main():
    print("=== Actualizando Controladores ===\n")
    
    updated = 0
    for entity, display_name in ENTITIES.items():
        if update_controller(entity, display_name):
            updated += 1
    
    print(f"\n=== {updated}/{len(ENTITIES)} controladores actualizados ===")

if __name__ == "__main__":
    main()
