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
    "Customers": "Cliente",
}

VIEWS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Views")

# Patrón para encontrar botones con onclick
ONCLICK_PATTERN = r'<button type="button" onclick="confirmDelete\(\'@Url\.Action\("Delete", new \{ id = item\.Id \}\)\', \'([^\']+)\'\)" class="action-btn delete border-0 bg-transparent" title="Eliminar"><i class="bi bi-trash"></i></button>'

def migrate_view(entity_name, display_name):
    index_file = VIEWS_DIR / entity_name / "Index.cshtml"
    
    if not index_file.exists():
        print(f"⚠ No encontrado: {index_file}")
        return False
    
    # Leer contenido
    content = index_file.read_text(encoding='utf-8')
    
    # Verificar si ya tiene data attributes
    if 'data-delete-url' in content:
        print(f"✓ {entity_name}: Ya usa data attributes")
        return True
    
    # Verificar si tiene onclick
    if 'confirmDelete' not in content:
        print(f"⚠ {entity_name}: No tiene botón de eliminación")
        return False
    
    # Crear backup
    backup_file = index_file.with_suffix('.cshtml.bak2')
    backup_file.write_text(content, encoding='utf-8')
    
    # Reemplazar onclick con data attributes
    new_button = f'<button type="button" data-delete-url="@Url.Action("Delete", new {{ id = item.Id }})" data-item-name="{display_name}" class="action-btn delete border-0 bg-transparent" title="Eliminar"><i class="bi bi-trash"></i></button>'
    
    new_content = re.sub(ONCLICK_PATTERN, new_button, content)
    
    if new_content != content:
        index_file.write_text(new_content, encoding='utf-8')
        print(f"✓ {entity_name}: Migrado a data attributes")
        return True
    else:
        print(f"⚠ {entity_name}: No se pudo migrar (patrón no coincide)")
        return False

def main():
    print("=== Migración de onclick a data attributes ===\n")
    
    migrated = 0
    for entity, display_name in ENTITIES.items():
        if migrate_view(entity, display_name):
            migrated += 1
    
    print(f"\n=== Completado: {migrated}/{len(ENTITIES)} vistas migradas ===")
    print("\nBackups creados con extensión .cshtml.bak2")

if __name__ == "__main__":
    main()
