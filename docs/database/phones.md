# Tabla: phones

## Descripción
Teléfonos de actores.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| actors_id | uuid | NO | null | FK a actors |
| phone_types_id | uuid | YES | null | FK a phone_types |
| extension | varchar | YES | null | Extensión telefónica |
| number | varchar | NO | null | Número telefónico |
| is_verified | boolean | YES | false | Teléfono verificado |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- actors_id → actors(id)
- phone_types_id → phone_types(id)
