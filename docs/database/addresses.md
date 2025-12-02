# Tabla: addresses

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v7() | Identificador único |
| actors_id | uuid | YES | null | Relación con otra tabla |
| address_types_id | uuid | YES | null | Relación con otra tabla |
| zip_codes_id | uuid | YES | null | Relación con otra tabla |
| street | varchar | YES | null |  |
| apartment | varchar | YES | null |  |
| is_verified | boolean | YES | false |  |
| latitude | numeric | YES | null |  |
| longitude | numeric | YES | null |  |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |
