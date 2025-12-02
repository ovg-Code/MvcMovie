# Tabla: customers

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v7() | Identificador único |
| actors_id | uuid | YES | null | Relación con otra tabla |
| customer_public_status_types_id | uuid | YES | null | Relación con otra tabla |
| is_agent_retention | boolean | YES | false |  |
| is_leasing | boolean | YES | false |  |
| other_data | json | YES | null |  |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |
