# Tabla: phones

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v7() | Identificador único |
| actors_id | uuid | YES | null | Relación con otra tabla |
| phone_types_id | uuid | YES | null | Relación con otra tabla |
| extension | varchar | YES | null |  |
| number | varchar | YES | null |  |
| is_verified | boolean | YES | false |  |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |
