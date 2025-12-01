# Tabla: relationship_types

## Descripción
Catálogo de tipos de relaciones entre actores.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| parent_id | uuid | YES | null | FK a relationship_types (jerárquico) |
| name | varchar | NO | null | Nombre del tipo |
| system_name | varchar | YES | null | Nombre del sistema |
| is_agent_retention | boolean | YES | false | Es agente de retención |
| is_leasing | boolean | YES | false | Es arrendamiento |
| is_percentage | boolean | YES | false | Es porcentaje |
| is_allowed | boolean | YES | true | Está permitido |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- parent_id → relationship_types(id)
