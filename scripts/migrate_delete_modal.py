#!/usr/bin/env python3
import os
import re
from pathlib import Path

# Mapeo de entidades a nombres descriptivos
ENTITIES = {
    "Actors": "Actor",
    "ActorRelationships": "Relación",
    "Phones": "Teléfono",
    "Emails": "Correo",
    "Addresses": "Dirección",
    "SocialNetworks": "Red Social",
    "IdentityCards": "Documento",
    "Countries": "País",
    "States": "Estado",
    "Cities": "Ciudad",
    "Municipalities": "Municipio",
    "Neighborhoods": "Barrio",
    "ZipCodes": "Código Postal",
    "ActorTypes": "Tipo de Actor",
    "AddressTypes": "Tipo de Dirección",
    "PhoneTypes": "Tipo de Teléfono",
    "IdentityCardTypes": "Tipo de Documento",
    "RelationshipTypes": "Tipo de Relación",
    "CustomerPublicStatusTypes": "Estado de Cliente",
    "Genders": "Género",
}

VIEWS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Views")

# Patrones para encontrar el enlace de eliminación (una línea y multilínea)
DELETE_LINK_PATTERN_SINGLE = r'<a\s+asp-action="Delete"\s+asp-route-id="@item\.Id"\s+class="action-btn delete"\s+title="Eliminar"><i\s+class="bi bi-trash"></i></a>'
DELETE_LINK_PATTERN_MULTI = r'<a\s+asp-action="Delete"\s+asp-route-id="@item\.Id"\s+class="action-btn delete"\s+title="Eliminar">\s*<i\s+class="bi bi-trash"></i>\s*</a>'

def migrate_view(entity_name, display_name):
    index_file = VIEWS_DIR / entity_name / "Index.cshtml"
    
    if not index_file.exists():
        print(f"⚠ No encontrado: {index_file}")
        return False
    
    # Leer contenido
    content = index_file.read_text(encoding='utf-8')
    
    # Verificar si ya tiene el modal
    if 'confirmDelete' in content:
        print(f"✓ {entity_name}: Ya migrado")
        return True
    
    # Crear backup
    backup_file = index_file.with_suffix('.cshtml.bak')
    backup_file.write_text(content, encoding='utf-8')
    
    # Reemplazar enlace con botón
    new_button = f'<button type="button" onclick="confirmDelete(\'@Url.Action("Delete", new {{ id = item.Id }})\', \'{display_name}\')" class="action-btn delete border-0 bg-transparent" title="Eliminar"><i class="bi bi-trash"></i></button>'
    
    # Intentar con patrón de una línea
    new_content = re.sub(DELETE_LINK_PATTERN_SINGLE, new_button, content)
    
    # Si no cambió, intentar con patrón multilínea
    if new_content == content:
        new_content = re.sub(DELETE_LINK_PATTERN_MULTI, new_button, content, flags=re.DOTALL)
    
    if new_content != content:
        index_file.write_text(new_content, encoding='utf-8')
        print(f"✓ {entity_name}: Migrado exitosamente")
        return True
    else:
        print(f"⚠ {entity_name}: No se encontró patrón de eliminación")
        return False

def main():
    print("=== Migración de Enlaces de Eliminación a Modal ===\n")
    
    migrated = 0
    for entity, display_name in ENTITIES.items():
        if migrate_view(entity, display_name):
            migrated += 1
    
    print(f"\n=== Completado: {migrated}/{len(ENTITIES)} vistas migradas ===")
    print("\nBackups creados con extensión .cshtml.bak")

if __name__ == "__main__":
    main()
