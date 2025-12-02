# Tabla: relationship_types

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v7() | Identificador único |
| parent_id | uuid | YES | null | Relación con otra tabla |
| name | varchar | YES | null |  |
| system_name | varchar | YES | null |  |
| is_percentage | boolean | YES | false |  |
| is_allowed | boolean | YES | true |  |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |
