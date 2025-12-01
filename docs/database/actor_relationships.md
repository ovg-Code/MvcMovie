# Tabla: actor_relationships

## Descripción
Relaciones entre actores.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| parent_id | uuid | NO | null | FK a actors (padre) |
| child_id | uuid | NO | null | FK a actors (hijo) |
| relationship_types_id | uuid | NO | null | FK a relationship_types |
| is_percentage | boolean | YES | false | Es porcentaje |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- parent_id → actors(id)
- child_id → actors(id)
- relationship_types_id → relationship_types(id)
