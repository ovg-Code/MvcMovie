# Tabla: customers

## Descripción
Clientes del sistema.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| actors_id | uuid | NO | null | FK a actors |
| customer_public_status_types_id | uuid | YES | null | FK a customer_public_status_types |
| other_data | json | YES | null | Datos adicionales |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- actors_id → actors(id)
- customer_public_status_types_id → customer_public_status_types(id)
