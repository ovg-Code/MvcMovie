# Tabla: addresses

## Descripción
Direcciones de actores.

## Estructura

| Columna | Tipo | Nulable | Default | Descripción |
|---------|------|---------|---------|-------------|
| id | uuid | NO | uuid_generate_v4() | Identificador único |
| actors_id | uuid | NO | null | FK a actors |
| address_types_id | uuid | YES | null | FK a address_types |
| zip_codes_id | uuid | YES | null | FK a zip_codes |
| street | varchar | YES | null | Calle |
| apartment | varchar | YES | null | Número/Apartamento |
| is_verified | boolean | YES | false | Dirección verificada |
| latitude | numeric | YES | null | Latitud |
| longitude | numeric | YES | null | Longitud |
| created_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha de creación |
| created_by | varchar | YES | null | Usuario creador |
| updated_at | timestamp | YES | CURRENT_TIMESTAMP | Fecha actualización |
| updated_by | varchar | YES | null | Usuario actualizador |
| is_enabled | boolean | YES | true | Registro activo |

## Relaciones
- actors_id → actors(id)
- address_types_id → address_types(id)
- zip_codes_id → zip_codes(id)
