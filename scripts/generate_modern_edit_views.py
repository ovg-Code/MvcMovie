#!/usr/bin/env python3
from pathlib import Path

VIEWS_DIR = Path("/mnt/c/Users/ovasquez/workspace/MvcMovie/Views")

# Configuración de cada entidad con sus campos editables (sin auditoría)
ENTITIES = {
    "ActorTypes": {
        "title": "Tipo de Actor",
        "icon": "tag",
        "fields": [
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "PhoneTypes": {
        "title": "Tipo de Teléfono",
        "icon": "tag",
        "fields": [
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "AddressTypes": {
        "title": "Tipo de Dirección",
        "icon": "tag",
        "fields": [
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "IdentityCardTypes": {
        "title": "Tipo de Documento",
        "icon": "tag",
        "fields": [
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "IsPrivate", "label": "Es Privado", "type": "bool", "col": 3},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 3}
        ]
    },
    "CustomerPublicStatusTypes": {
        "title": "Estado de Cliente",
        "icon": "tag",
        "fields": [
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "SystemName", "label": "Nombre del Sistema", "type": "text", "col": 6},
            {"name": "Order", "label": "Orden", "type": "number", "col": 4},
            {"name": "IsPrivate", "label": "Es Privado", "type": "bool", "col": 4},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 4}
        ]
    },
    "Countries": {
        "title": "País",
        "icon": "flag",
        "fields": [
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "States": {
        "title": "Estado",
        "icon": "map",
        "fields": [
            {"name": "CountriesId", "label": "País", "type": "select", "col": 6, "viewbag": "Countries"},
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "Cities": {
        "title": "Ciudad",
        "icon": "building",
        "fields": [
            {"name": "StatesId", "label": "Estado", "type": "select", "col": 6, "viewbag": "States"},
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "Municipalities": {
        "title": "Municipio",
        "icon": "building",
        "fields": [
            {"name": "CitiesId", "label": "Ciudad", "type": "select", "col": 6, "viewbag": "Cities"},
            {"name": "Name", "label": "Nombre", "type": "text", "col": 4},
            {"name": "Code", "label": "Código", "type": "text", "col": 2},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "Neighborhoods": {
        "title": "Barrio",
        "icon": "houses",
        "fields": [
            {"name": "MunicipalitiesId", "label": "Municipio", "type": "select", "col": 6, "viewbag": "Municipalities"},
            {"name": "Name", "label": "Nombre", "type": "text", "col": 4},
            {"name": "Code", "label": "Código", "type": "text", "col": 2},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "Phones": {
        "title": "Teléfono",
        "icon": "telephone",
        "fields": [
            {"name": "ActorsId", "label": "Actor", "type": "select", "col": 6, "viewbag": "Actors"},
            {"name": "PhoneTypesId", "label": "Tipo", "type": "select", "col": 6, "viewbag": "PhoneTypes"},
            {"name": "Number", "label": "Número", "type": "text", "col": 6},
            {"name": "Extension", "label": "Extensión", "type": "text", "col": 6},
            {"name": "IsVerified", "label": "Verificado", "type": "bool", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "Emails": {
        "title": "Correo",
        "icon": "envelope",
        "fields": [
            {"name": "ActorsId", "label": "Actor", "type": "select", "col": 6, "viewbag": "Actors"},
            {"name": "Definition", "label": "Correo Electrónico", "type": "email", "col": 6},
            {"name": "IsNotification", "label": "Notificaciones", "type": "bool", "col": 3},
            {"name": "IsPrimary", "label": "Principal", "type": "bool", "col": 3},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 3}
        ]
    },
    "Addresses": {
        "title": "Dirección",
        "icon": "geo-alt",
        "fields": [
            {"name": "ActorsId", "label": "Actor", "type": "select", "col": 6, "viewbag": "Actors"},
            {"name": "AddressTypesId", "label": "Tipo", "type": "select", "col": 6, "viewbag": "AddressTypes"},
            {"name": "ZipCodesId", "label": "Código Postal", "type": "select", "col": 6, "viewbag": "ZipCodes"},
            {"name": "Street", "label": "Calle", "type": "text", "col": 6},
            {"name": "Apartment", "label": "Apartamento", "type": "text", "col": 6},
            {"name": "IsVerified", "label": "Verificada", "type": "bool", "col": 3},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 3}
        ]
    },
    "IdentityCards": {
        "title": "Documento",
        "icon": "card-text",
        "fields": [
            {"name": "ActorsId", "label": "Actor", "type": "select", "col": 6, "viewbag": "Actors"},
            {"name": "IdcardTypesId", "label": "Tipo", "type": "select", "col": 6, "viewbag": "IdentityCardTypes"},
            {"name": "Idcard", "label": "Número de Documento", "type": "text", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "SocialNetworks": {
        "title": "Red Social",
        "icon": "share",
        "fields": [
            {"name": "ActorsId", "label": "Actor", "type": "select", "col": 6, "viewbag": "Actors"},
            {"name": "Platform", "label": "Plataforma", "type": "text", "col": 6},
            {"name": "ProfileName", "label": "Nombre de Perfil", "type": "text", "col": 6},
            {"name": "ProfileUrl", "label": "URL del Perfil", "type": "url", "col": 6},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 6}
        ]
    },
    "ActorRelationships": {
        "title": "Relación",
        "icon": "people",
        "fields": [
            {"name": "ParentId", "label": "Actor Principal", "type": "select", "col": 6, "viewbag": "ParentActors"},
            {"name": "ChildId", "label": "Actor Relacionado", "type": "select", "col": 6, "viewbag": "ChildActors"},
            {"name": "RelationshipTypesId", "label": "Tipo de Relación", "type": "select", "col": 6, "viewbag": "RelationshipTypes"},
            {"name": "IsPercentage", "label": "Es Porcentaje", "type": "bool", "col": 3},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 3}
        ]
    },
    "RelationshipTypes": {
        "title": "Tipo de Relación",
        "icon": "tag",
        "fields": [
            {"name": "ParentId", "label": "Tipo Padre", "type": "select", "col": 6, "viewbag": "ParentTypes"},
            {"name": "Name", "label": "Nombre", "type": "text", "col": 6},
            {"name": "SystemName", "label": "Nombre del Sistema", "type": "text", "col": 6},
            {"name": "IsPercentage", "label": "Es Porcentaje", "type": "bool", "col": 4},
            {"name": "IsAllowed", "label": "Permitido", "type": "bool", "col": 4},
            {"name": "IsEnabled", "label": "Estado", "type": "bool", "col": 4}
        ]
    }
}

def generate_field_html(field):
    """Genera el HTML para un campo del formulario"""
    col = field.get('col', 6)
    name = field['name']
    label = field['label']
    field_type = field.get('type', 'text')
    
    html = f'                <div class="col-md-{col}">\n'
    html += f'                    <label asp-for="{name}" class="form-label">{label}</label>\n'
    
    if field_type == 'bool':
        html += f'                    <select asp-for="{name}" class="form-select">\n'
        html += f'                        <option value="">Seleccione...</option>\n'
        html += f'                        <option value="true">Sí</option>\n'
        html += f'                        <option value="false">No</option>\n'
        html += f'                    </select>\n'
    elif field_type == 'select':
        viewbag = field.get('viewbag', '')
        html += f'                    <select asp-for="{name}" asp-items="ViewBag.{viewbag}" class="form-select">\n'
        html += f'                        <option value="">Seleccione...</option>\n'
        html += f'                    </select>\n'
    elif field_type == 'number':
        html += f'                    <input asp-for="{name}" type="number" class="form-control" />\n'
    elif field_type == 'email':
        html += f'                    <input asp-for="{name}" type="email" class="form-control" />\n'
    elif field_type == 'url':
        html += f'                    <input asp-for="{name}" type="url" class="form-control" />\n'
    else:
        html += f'                    <input asp-for="{name}" class="form-control" />\n'
    
    html += f'                    <span asp-validation-for="{name}" class="text-danger"></span>\n'
    html += f'                </div>\n'
    
    return html

def generate_edit_view(entity, config):
    """Genera la vista Edit completa"""
    title = config['title']
    icon = config['icon']
    fields = config['fields']
    
    # Generar campos HTML
    fields_html = ""
    for field in fields:
        fields_html += generate_field_html(field)
    
    view = f'''@model ari2._0.Models.{entity.rstrip('s') if entity.endswith('s') and entity != 'Addresses' else entity}
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
        <div class="form-section">
            <h6 class="form-section-title"><i class="bi bi-{icon}"></i>Información</h6>
            <div class="row g-3">
{fields_html.rstrip()}
            </div>
        </div>

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

def main():
    print("=== Generando Vistas Edit Modernizadas ===\n")
    
    for entity, config in ENTITIES.items():
        edit_file = VIEWS_DIR / entity / "Edit.cshtml"
        
        view_content = generate_edit_view(entity, config)
        edit_file.write_text(view_content, encoding='utf-8')
        
        print(f"✓ {entity}: Vista Edit generada")
    
    print(f"\n✓ {len(ENTITIES)} vistas Edit modernizadas")

if __name__ == "__main__":
    main()
