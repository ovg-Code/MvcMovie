# Tabla: actors

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v7() | Identificador único |
| actor_types_id | uuid | YES | null | Relación con otra tabla |
| genders_id | uuid | YES | null | Relación con otra tabla |
| nationality_countries_id | uuid | YES | null | Relación con otra tabla |
| title | varchar | YES | null |  |
| prefix | varchar | YES | null |  |
| suffix | varchar | YES | null |  |
| is_pep | varchar | YES | null |  |
| first_first_name | varchar | YES | null |  |
| second_first_name | varchar | YES | null |  |
| last_first_name | varchar | YES | null |  |
| last_second_name | varchar | YES | null |  |
| birthday_at | date | YES | null |  |
| other_data | json | YES | null |  |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |
