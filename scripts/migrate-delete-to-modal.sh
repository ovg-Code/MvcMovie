#!/bin/bash

# Script para migrar enlaces de eliminación a modal en todas las vistas Index
# Uso: ./migrate-delete-to-modal.sh

VIEWS_DIR="/mnt/c/Users/ovasquez/workspace/MvcMovie/Views"

# Array de entidades con sus nombres descriptivos
declare -A ENTITIES=(
    ["Actors"]="Actor"
    ["ActorRelationships"]="Relación"
    ["Phones"]="Teléfono"
    ["Emails"]="Correo"
    ["Addresses"]="Dirección"
    ["SocialNetworks"]="Red Social"
    ["IdentityCards"]="Documento"
    ["Countries"]="País"
    ["States"]="Estado"
    ["Cities"]="Ciudad"
    ["Municipalities"]="Municipio"
    ["Neighborhoods"]="Barrio"
    ["ZipCodes"]="Código Postal"
    ["ActorTypes"]="Tipo de Actor"
    ["AddressTypes"]="Tipo de Dirección"
    ["PhoneTypes"]="Tipo de Teléfono"
    ["IdentityCardTypes"]="Tipo de Documento"
    ["RelationshipTypes"]="Tipo de Relación"
    ["CustomerPublicStatusTypes"]="Estado de Cliente"
    ["Genders"]="Género"
)

echo "=== Migración de Enlaces de Eliminación a Modal ==="
echo ""

for entity in "${!ENTITIES[@]}"; do
    INDEX_FILE="$VIEWS_DIR/$entity/Index.cshtml"
    DELETE_FILE="$VIEWS_DIR/$entity/Delete.cshtml"
    
    if [ -f "$INDEX_FILE" ]; then
        echo "Procesando: $entity..."
        
        # Crear backup
        cp "$INDEX_FILE" "$INDEX_FILE.bak"
        
        # Reemplazar el enlace de eliminación con el botón del modal
        sed -i "s|<a asp-action=\"Delete\" asp-route-id=\"@item.Id\" class=\"action-btn delete\" title=\"Eliminar\"><i class=\"bi bi-trash\"></i></a>|<button type=\"button\" onclick=\"confirmDelete('@Url.Action(\"Delete\", new { id = item.Id })', '${ENTITIES[$entity]}')" class=\"action-btn delete border-0 bg-transparent\" title=\"Eliminar\"><i class=\"bi bi-trash\"></i></button>|g" "$INDEX_FILE"
        
        echo "  ✓ Vista Index actualizada"
        
        # Eliminar vista Delete.cshtml si existe
        if [ -f "$DELETE_FILE" ]; then
            mv "$DELETE_FILE" "$DELETE_FILE.bak"
            echo "  ✓ Vista Delete.cshtml respaldada (puede eliminarse)"
        fi
        
        echo ""
    else
        echo "⚠ No se encontró: $INDEX_FILE"
        echo ""
    fi
done

echo "=== Migración Completada ==="
echo ""
echo "Notas:"
echo "- Se crearon backups con extensión .bak"
echo "- Las vistas Delete.cshtml fueron respaldadas"
echo "- Revisa los cambios antes de eliminar los backups"
echo ""
echo "Para eliminar los backups:"
echo "  find $VIEWS_DIR -name '*.bak' -delete"
