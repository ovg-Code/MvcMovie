# Tabla: social_networks

## Descripción
Redes sociales de actores.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| actors_id | uuid | NO | null | FK a actors |
| platform | varchar | NO | null | Plataforma (Facebook, Twitter, etc) |
| profile_name | varchar | YES | null | Nombre de perfil |
| profile_url | varchar | YES | null | URL del perfil |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- actors_id → actors(id)
