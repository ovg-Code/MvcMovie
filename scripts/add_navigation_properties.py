#!/usr/bin/env python3
from pathlib import Path

MODELS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Models")
REPOS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Repositories")
VIEWS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Views")

# Configuración de navigation properties por modelo
CONFIGS = {
    "Email": {
        "nav_props": [
            ("ActorsId", "Actor", "Actor")
        ],
        "view_updates": [
            ("item.ActorsId?.ToString()", "item.Actor != null ? $\"{item.Actor.FirstFirstName} {item.Actor.LastFirstName}\" : \"-\"")
        ]
    },
    "Address": {
        "nav_props": [
            ("ActorsId", "Actor", "Actor"),
            ("AddressTypesId", "AddressType", "AddressType"),
            ("ZipCodesId", "ZipCode", "ZipCode")
        ],
        "view_updates": [
            ("item.ActorsId?.ToString()", "item.Actor != null ? $\"{item.Actor.FirstFirstName} {item.Actor.LastFirstName}\" : \"-\""),
            ("item.AddressTypesId?.ToString()", "item.AddressType?.Name"),
            ("item.ZipCodesId?.ToString()", "item.ZipCode?.Name")
        ]
    },
    "IdentityCard": {
        "nav_props": [
            ("ActorsId", "Actor", "Actor"),
            ("IdcardTypesId", "IdentityCardType", "IdentityCardType")
        ],
        "view_updates": [
            ("item.ActorsId.ToString()", "item.Actor != null ? $\"{item.Actor.FirstFirstName} {item.Actor.LastFirstName}\" : \"-\""),
            ("item.IdcardTypesId.ToString()", "item.IdentityCardType?.Name")
        ]
    },
    "SocialNetwork": {
        "nav_props": [
            ("ActorsId", "Actor", "Actor")
        ],
        "view_updates": [
            ("item.ActorsId?.ToString()", "item.Actor != null ? $\"{item.Actor.FirstFirstName} {item.Actor.LastFirstName}\" : \"-\"")
        ]
    },
    "ActorRelationship": {
        "nav_props": [
            ("ParentId", "ParentActor", "Actor"),
            ("ChildId", "ChildActor", "Actor"),
            ("RelationshipTypesId", "RelationshipType", "RelationshipType")
        ],
        "view_updates": [
            ("item.ParentId.ToString()", "item.ParentActor != null ? $\"{item.ParentActor.FirstFirstName} {item.ParentActor.LastFirstName}\" : \"-\""),
            ("item.ChildId.ToString()", "item.ChildActor != null ? $\"{item.ChildActor.FirstFirstName} {item.ChildActor.LastFirstName}\" : \"-\""),
            ("item.RelationshipTypesId.ToString()", "item.RelationshipType?.Name")
        ]
    }
}

def add_nav_props_to_model(model_name, nav_props):
    """Agrega navigation properties al modelo"""
    model_file = MODELS_DIR / f"{model_name}.cs"
    if not model_file.exists():
        return False
    
    content = model_file.read_text(encoding='utf-8')
    
    # Buscar última propiedad antes del cierre
    lines = content.split('\n')
    insert_idx = None
    for i in range(len(lines) - 1, -1, -1):
        if 'public bool? IsEnabled' in lines[i] or 'public DateTime?' in lines[i]:
            insert_idx = i + 1
            break
    
    if insert_idx is None:
        return False
    
    # Agregar navigation properties
    nav_lines = ["\n    // Navigation properties"]
    for fk_prop, nav_name, nav_type in nav_props:
        nav_lines.append(f"    public virtual {nav_type}? {nav_name} {{ get; set; }}")
    
    lines.insert(insert_idx, '\n'.join(nav_lines))
    model_file.write_text('\n'.join(lines), encoding='utf-8')
    return True

def update_repository(model_name, nav_props):
    """Actualiza repository con Include"""
    repo_file = REPOS_DIR / f"{model_name}Repository.cs"
    if not repo_file.exists():
        return False
    
    content = repo_file.read_text(encoding='utf-8')
    
    # Agregar using si no existe
    if 'using Microsoft.EntityFrameworkCore;' not in content:
        content = 'using Microsoft.EntityFrameworkCore;\n' + content
    
    # Crear método GetAllAsync con Include
    includes = '\n'.join([f"            .Include(x => x.{nav_name})" for _, nav_name, _ in nav_props])
    
    override_method = f'''
    public override async Task<IEnumerable<{model_name}>> GetAllAsync()
    {{
        return await _dbSet
{includes}
            .ToListAsync();
    }}
}}'''
    
    # Reemplazar cierre de clase
    content = content.rstrip().rstrip('}') + override_method
    
    repo_file.write_text(content, encoding='utf-8')
    return True

def update_view(model_name, view_updates):
    """Actualiza vista Index con navigation properties"""
    view_file = VIEWS_DIR / f"{model_name}s/Index.cshtml"
    if not view_file.exists():
        view_file = VIEWS_DIR / f"{model_name}/Index.cshtml"
    if not view_file.exists():
        return False
    
    content = view_file.read_text(encoding='utf-8')
    
    for old, new in view_updates:
        content = content.replace(old, new)
    
    view_file.write_text(content, encoding='utf-8')
    return True

def main():
    print("=== Agregando Navigation Properties ===\n")
    
    for model_name, config in CONFIGS.items():
        print(f"Procesando {model_name}...")
        
        if add_nav_props_to_model(model_name, config['nav_props']):
            print(f"  ✓ Modelo actualizado")
        
        if update_repository(model_name, config['nav_props']):
            print(f"  ✓ Repository actualizado")
        
        if update_view(model_name, config['view_updates']):
            print(f"  ✓ Vista actualizada")
        
        print()
    
    print("✓ Completado")

if __name__ == "__main__":
    main()
