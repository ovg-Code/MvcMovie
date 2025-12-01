# Tabla: actors

## Descripción
Almacena información de actores (personas físicas o jurídicas).

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| actor_types_id | uuid | YES | null | FK a actor_types |
| genders_id | uuid | YES | null | FK a genders |
| nationality_countries_id | uuid | YES | null | FK a countries |
| title | varchar | YES | null | Título |
| prefix | varchar | YES | null | Prefijo |
| suffix | varchar | YES | null | Sufijo |
| is_pep | varchar | YES | null | Persona Expuesta Políticamente |
| first_first_name | varchar | YES | null | Primer nombre |
| second_first_name | varchar | YES | null | Segundo nombre |
| last_first_name | varchar | YES | null | Primer apellido |
| last_second_name | varchar | YES | null | Segundo apellido |
| birthday_at | date | YES | null | Fecha de nacimiento |
| other_data | json | YES | null | Datos adicionales |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- actor_types_id → actor_types(id)
- genders_id → genders(id)
- nationality_countries_id → countries(id)
