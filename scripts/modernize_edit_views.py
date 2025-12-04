#!/usr/bin/env python3
from pathlib import Path

VIEWS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Views")

# Entidades que necesitan modernización
ENTITIES = {
    "Actors": {"title": "Actor", "icon": "person"},
    "ActorTypes": {"title": "Tipo de Actor", "icon": "tag"},
    "ActorRelationships": {"title": "Relación", "icon": "people"},
    "Addresses": {"title": "Dirección", "icon": "geo-alt"},
    "AddressTypes": {"title": "Tipo de Dirección", "icon": "tag"},
    "Cities": {"title": "Ciudad", "icon": "building"},
    "Countries": {"title": "País", "icon": "flag"},
    "CustomerPublicStatusTypes": {"title": "Estado de Cliente", "icon": "tag"},
    "Emails": {"title": "Correo", "icon": "envelope"},
    "Genders": {"title": "Género", "icon": "tag"},
    "IdentityCards": {"title": "Documento", "icon": "card-text"},
    "IdentityCardTypes": {"title": "Tipo de Documento", "icon": "tag"},
    "Municipalities": {"title": "Municipio", "icon": "building"},
    "Neighborhoods": {"title": "Barrio", "icon": "houses"},
    "Phones": {"title": "Teléfono", "icon": "telephone"},
    "PhoneTypes": {"title": "Tipo de Teléfono", "icon": "tag"},
    "RelationshipTypes": {"title": "Tipo de Relación", "icon": "tag"},
    "SocialNetworks": {"title": "Red Social", "icon": "share"},
    "States": {"title": "Estado", "icon": "map"},
}

def check_if_modern(file_path):
    """Verifica si la vista ya tiene el diseño moderno"""
    content = file_path.read_text(encoding='utf-8')
    return '_LayoutDashboard' in content and 'form-views.css' in content

def modernize_edit(entity, config):
    edit_file = VIEWS_DIR / entity / "Edit.cshtml"
    
    if not edit_file.exists():
        print(f"⚠ No existe: {entity}/Edit.cshtml")
        return False
    
    if check_if_modern(edit_file):
        print(f"✓ {entity}: Ya modernizado")
        return True
    
    # Leer contenido actual para extraer campos del formulario
    old_content = edit_file.read_text(encoding='utf-8')
    
    # Verificar si tiene campos de auditoría que no deberían estar
    has_audit_fields = any(field in old_content for field in ['CreatedAt', 'CreatedBy', 'UpdatedAt', 'UpdatedBy'])
    
    print(f"🔄 {entity}: Modernizando...")
    if has_audit_fields:
        print(f"   ⚠ Tiene campos de auditoría (se deben remover manualmente)")
    
    return True

def main():
    print("=== Análisis de Vistas Edit ===\n")
    
    needs_update = []
    for entity, config in ENTITIES.items():
        if not modernize_edit(entity, config):
            needs_update.append(entity)
    
    if needs_update:
        print(f"\n⚠ {len(needs_update)} vistas necesitan actualización manual")
    else:
        print("\n✓ Análisis completado")

if __name__ == "__main__":
    main()
