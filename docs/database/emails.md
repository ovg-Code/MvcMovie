# Tabla: emails

## Descripción
Correos electrónicos de actores.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| actors_id | uuid | NO | null | FK a actors |
| definition | varchar | NO | null | Dirección de email |
| is_notification | boolean | YES | false | Recibe notificaciones |
| is_unsuscribed | boolean | YES | false | Desuscrito |
| unsuscribed_at | timestamp | YES | null | Fecha de desuscripción |
| is_failed | boolean | YES | false | Email fallido |
| faliled_at | timestamp | YES | null | Fecha de fallo |
| is_primary | boolean | YES | false | Email principal |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- actors_id → actors(id)
