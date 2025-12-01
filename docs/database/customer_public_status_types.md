# Tabla: customer_public_status_types

## Descripción
Catálogo de estados públicos de clientes.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| name | varchar | NO | null | Nombre del estado |
| system_name | varchar | YES | null | Nombre del sistema |
| order | integer | YES | null | Orden de visualización |
| is_private | boolean | YES | false | Estado privado |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |
