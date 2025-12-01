#!/bin/bash

# Script para obtener la estructura real de todas las tablas desde PostgreSQL
# Genera archivos .md con la estructura actual de cada tabla

DB_HOST="ariv2-crm-db.curms68ogomm.us-east-1.rds.amazonaws.com"
DB_PORT="5432"
DB_NAME="ariv2"
DB_USER="postgres"
DB_PASS="Ariv2025!"

TABLES=(
    "countries"
    "genders"
    "actor_types"
    "phone_types"
    "address_types"
    "states"
    "cities"
    "municipalities"
    "neighborhoods"
    "zip_codes"
    "actors"
    "customers"
    "phones"
    "emails"
    "addresses"
    "identity_cards"
    "identity_card_types"
    "actor_relationships"
    "relationship_types"
    "social_networks"
    "customer_public_status_types"
)

echo "Conectando a PostgreSQL para obtener estructura real de las tablas..."
echo "Host: $DB_HOST"
echo "Database: $DB_NAME"
echo ""

for table in "${TABLES[@]}"; do
    echo "Obteniendo estructura de: $table"
    
    PGPASSWORD=$DB_PASS psql -h $DB_HOST -p $DB_PORT -U $DB_USER -d $DB_NAME -c "\d $table" > "../docs/database/${table}_structure.txt"
    
    if [ $? -eq 0 ]; then
        echo "✓ $table - OK"
    else
        echo "✗ $table - ERROR"
    fi
done

echo ""
echo "Archivos generados en docs/database/"
echo "Revisa los archivos *_structure.txt para ver la estructura real"
