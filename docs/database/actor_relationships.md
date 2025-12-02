# Tabla: actor_relationships

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v7() | Identificador único |
| parent_id | uuid | YES | null | Relación con otra tabla |
| child_id | uuid | YES | null | Relación con otra tabla |
| relationship_types_id | uuid | YES | null | Relación con otra tabla |
| is_percentage | boolean | YES | false |  |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |
