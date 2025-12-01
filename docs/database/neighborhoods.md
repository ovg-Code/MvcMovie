# Tabla: neighborhoods

## Descripción
Catálogo de colonias/barrios.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| municipalities_id | uuid | NO | null | FK a municipalities |
| name | varchar | NO | null | Nombre de la colonia |
| code | varchar | YES | null | Código de la colonia |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- municipalities_id → municipalities(id)
