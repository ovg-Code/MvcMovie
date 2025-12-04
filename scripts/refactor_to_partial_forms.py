#!/usr/bin/env python3
from pathlib import Path
import re

VIEWS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Views")

ENTITIES = [
    "Genders", "ActorTypes", "PhoneTypes", "AddressTypes", "IdentityCardTypes",
    "CustomerPublicStatusTypes", "Countries", "States", "Cities", "Municipalities",
    "Neighborhoods", "Phones", "Emails", "Addresses", "IdentityCards",
    "SocialNetworks", "ActorRelationships", "RelationshipTypes", "Actors", "ZipCodes"
]

def extract_form_content(edit_file):
    """Extrae el contenido del formulario de la vista Edit"""
    content = edit_file.read_text(encoding='utf-8')
    
    # Buscar desde <div class="form-container"> hasta </div> antes de form-actions
    match = re.search(r'(<div class="form-section">.*?)</div>\s*<div class="form-actions">', content, re.DOTALL)
    if match:
        return match.group(1) + '</div>'
    return None

def create_partial_form(entity, form_content):
    """Crea el partial view _Form.cshtml"""
    entity_dir = VIEWS_DIR / entity
    partial_file = entity_dir / "_Form.cshtml"
    
    partial_file.write_text(form_content, encoding='utf-8')
    return True

def create_simple_create(entity, title, icon):
    """Crea vista Create simplificada que usa el partial"""
    model_name = get_model_name(entity)
    
    view = f'''@model ari2._0.Models.{model_name}
@{{
    ViewData["Title"] = "Nuevo {title}";
    Layout = "_LayoutDashboard";
}}
@section Styles {{
    <link rel="stylesheet" href="~/css/views/form-views.css" asp-append-version="true" />
}}

<div class="d-flex justify-content-between align-items-center mb-4">
    <div>
        <h1 class="page-title mb-1">Nuevo {title}</h1>
        <p class="page-subtitle mb-0">Complete la información del registro</p>
    </div>
    <a asp-action="Index" class="btn btn-secondary-custom"><i class="bi bi-arrow-left me-2"></i>Volver</a>
</div>

<form asp-action="Create" method="post">
    <div asp-validation-summary="ModelOnly" class="alert alert-danger" role="alert"></div>
    
    <div class="form-container">
        @await Html.PartialAsync("_Form")

        <div class="form-actions">
            <button type="submit" class="btn btn-primary-custom"><i class="bi bi-check-lg me-2"></i>Guardar</button>
            <a asp-action="Index" class="btn btn-secondary-custom"><i class="bi bi-x-lg me-2"></i>Cancelar</a>
        </div>
    </div>
</form>

@section Scripts {{
    @{{await Html.RenderPartialAsync("_ValidationScriptsPartial");}}
}}
'''
    return view

def create_simple_edit(entity, title, icon):
    """Crea vista Edit simplificada que usa el partial"""
    model_name = get_model_name(entity)
    
    view = f'''@model ari2._0.Models.{model_name}
@{{
    ViewData["Title"] = "Editar {title}";
    Layout = "_LayoutDashboard";
}}
@section Styles {{
    <link rel="stylesheet" href="~/css/views/form-views.css" asp-append-version="true" />
}}

<div class="d-flex justify-content-between align-items-center mb-4">
    <div>
        <h1 class="page-title mb-1">Editar {title}</h1>
        <p class="page-subtitle mb-0">Modifique la información del registro</p>
    </div>
    <a asp-action="Index" class="btn btn-secondary-custom"><i class="bi bi-arrow-left me-2"></i>Volver</a>
</div>

<form asp-action="Edit" method="post">
    <div asp-validation-summary="ModelOnly" class="alert alert-danger" role="alert"></div>
    <input type="hidden" asp-for="Id" />
    
    <div class="form-container">
        @await Html.PartialAsync("_Form")

        <div class="form-actions">
            <button type="submit" class="btn btn-primary-custom"><i class="bi bi-check-lg me-2"></i>Guardar Cambios</button>
            <a asp-action="Index" class="btn btn-secondary-custom"><i class="bi bi-x-lg me-2"></i>Cancelar</a>
        </div>
    </div>
</form>

@section Scripts {{
    @{{await Html.RenderPartialAsync("_ValidationScriptsPartial");}}
}}
'''
    return view

def get_model_name(entity):
    """Obtiene el nombre correcto del modelo"""
    singular_map = {
        "Genders": "Gender",
        "ActorTypes": "ActorType",
        "PhoneTypes": "PhoneType",
        "AddressTypes": "AddressType",
        "IdentityCardTypes": "IdentityCardType",
        "CustomerPublicStatusTypes": "CustomerPublicStatusType",
        "Countries": "Country",
        "States": "State",
        "Cities": "City",
        "Municipalities": "Municipality",
        "Neighborhoods": "Neighborhood",
        "Phones": "Phone",
        "Emails": "Email",
        "Addresses": "Address",
        "IdentityCards": "IdentityCard",
        "SocialNetworks": "SocialNetwork",
        "ActorRelationships": "ActorRelationship",
        "RelationshipTypes": "RelationshipType",
        "Actors": "Actor",
        "ZipCodes": "ZipCode"
    }
    return singular_map.get(entity, entity)

def get_entity_config(entity):
    """Obtiene configuración de la entidad"""
    configs = {
        "Genders": {"title": "Género", "icon": "tag"},
        "ActorTypes": {"title": "Tipo de Actor", "icon": "tag"},
        "PhoneTypes": {"title": "Tipo de Teléfono", "icon": "tag"},
        "AddressTypes": {"title": "Tipo de Dirección", "icon": "tag"},
        "IdentityCardTypes": {"title": "Tipo de Documento", "icon": "tag"},
        "CustomerPublicStatusTypes": {"title": "Estado de Cliente", "icon": "tag"},
        "Countries": {"title": "País", "icon": "flag"},
        "States": {"title": "Estado", "icon": "map"},
        "Cities": {"title": "Ciudad", "icon": "building"},
        "Municipalities": {"title": "Municipio", "icon": "building"},
        "Neighborhoods": {"title": "Barrio", "icon": "houses"},
        "Phones": {"title": "Teléfono", "icon": "telephone"},
        "Emails": {"title": "Correo", "icon": "envelope"},
        "Addresses": {"title": "Dirección", "icon": "geo-alt"},
        "IdentityCards": {"title": "Documento", "icon": "card-text"},
        "SocialNetworks": {"title": "Red Social", "icon": "share"},
        "ActorRelationships": {"title": "Relación", "icon": "people"},
        "RelationshipTypes": {"title": "Tipo de Relación", "icon": "tag"},
        "Actors": {"title": "Actor", "icon": "person"},
        "ZipCodes": {"title": "Código Postal", "icon": "geo-alt"}
    }
    return configs.get(entity, {"title": entity, "icon": "tag"})

def refactor_entity(entity):
    """Refactoriza una entidad para usar partial forms"""
    entity_dir = VIEWS_DIR / entity
    edit_file = entity_dir / "Edit.cshtml"
    
    if not edit_file.exists():
        print(f"⚠ {entity}: No existe Edit.cshtml")
        return False
    
    # Extraer contenido del formulario
    form_content = extract_form_content(edit_file)
    if not form_content:
        print(f"⚠ {entity}: No se pudo extraer formulario")
        return False
    
    # Crear partial _Form.cshtml
    create_partial_form(entity, form_content)
    
    # Obtener configuración
    config = get_entity_config(entity)
    
    # Crear nuevas vistas Create y Edit
    create_view = create_simple_create(entity, config["title"], config["icon"])
    edit_view = create_simple_edit(entity, config["title"], config["icon"])
    
    (entity_dir / "Create.cshtml").write_text(create_view, encoding='utf-8')
    (entity_dir / "Edit.cshtml").write_text(edit_view, encoding='utf-8')
    
    print(f"✓ {entity}: Refactorizado")
    return True

def main():
    print("=== Refactorizando a Partial Forms ===\n")
    
    success = 0
    for entity in ENTITIES:
        if refactor_entity(entity):
            success += 1
    
    print(f"\n✓ {success}/{len(ENTITIES)} entidades refactorizadas")
    print("\nAhora cada entidad tiene:")
    print("  - _Form.cshtml (partial compartido)")
    print("  - Create.cshtml (usa el partial)")
    print("  - Edit.cshtml (usa el partial)")

if __name__ == "__main__":
    main()
