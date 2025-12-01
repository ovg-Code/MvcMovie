#!/bin/bash

# Script para generar controladores con scaffolding oficial de Microsoft
# Uso: ./generate_controller.sh ModelName

if [ -z "$1" ]; then
    echo "Error: Debes proporcionar el nombre del modelo"
    echo "Uso: ./generate_controller.sh ModelName"
    echo "Ejemplo: ./generate_controller.sh Product"
    exit 1
fi

MODEL=$1
CONTROLLER="${MODEL}sController"

echo "Generando controlador para modelo: $MODEL"
echo "Controlador: $CONTROLLER"

dotnet aspnet-codegenerator controller \
  -name $CONTROLLER \
  -m $MODEL \
  -dc ApplicationDbContext \
  --relativeFolderPath Controllers \
  --useDefaultLayout \
  --referenceScriptLibraries \
  --databaseProvider postgres

if [ $? -eq 0 ]; then
    echo "✅ Controlador generado exitosamente"
    echo "📝 No olvides agregar el comando a docs/development/SCAFFOLDING_HISTORY.md"
else
    echo "❌ Error al generar el controlador"
    exit 1
fi
