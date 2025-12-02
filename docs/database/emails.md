# Tabla: emails

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v7() | Identificador único |
| actors_id | uuid | YES | null | Relación con otra tabla |
| definition | varchar | YES | null |  |
| is_notification | boolean | YES | false |  |
| is_unsuscribed | boolean | YES | false |  |
| unsuscribed_at | timestamp | YES | null |  |
| is_failed | boolean | YES | false |  |
| faliled_at | timestamp | YES | null |  |
| is_primary | boolean | YES | false |  |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |
