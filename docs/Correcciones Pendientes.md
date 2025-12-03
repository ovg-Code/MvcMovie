# Correcciones Pendientes

Documento de seguimiento de ajustes identificados en base a los requerimientos del Modulo de Cliente.

Fecha de revision: 03/12/2025

---

## Estado de Cumplimiento

### Cumple Correctamente

| Requerimiento | Estado | Referencia |
|---------------|--------|------------|
| Patron MVC | Cumple | Controllers, Models, Views separados |
| Patron Repository | Cumple | Interfaces + implementaciones genericas |
| Capa de Services | Cumple | Abstraccion entre Controller y Repository |
| Campos de auditoria | Cumple | created_at, created_by, updated_at, updated_by en todas las tablas |
| UUID PostgreSQL friendly | Cumple | Usando UUIDNext con Database.PostgreSql |
| Nomenclatura tablas plural | Cumple | Actors, Customers, Phones, etc. |
| Booleanos con is_ | Cumple | IsEnabled, IsVerified, IsPep, etc. |
| Timestamps con _at | Cumple | CreatedAt, UpdatedAt, VerifiedAt |
| Campo other_data (JSON) | Cumple | En Actor y Customer |
| Tabla social_networks | Cumple | Implementada |
| Coordenadas en addresses | Cumple | Latitude, Longitude integrados |
| Campos de phones | Cumple | Number, Extension, IsVerified |
| Campos de actors | Cumple | Nationality, Title, IsPep, Prefix, Suffix |

---

## Correcciones Requeridas

### 1. Actor.cs - Campo BirthdayAt

- **Archivo**: `Models/Actor.cs`
- **Linea**: 19
- **Problema**: El campo se llama `BirthdayAt`
- **Correccion**: Debe ser `BirthDate` segun requerimiento linea 43: "birth_date en lugar de birthday"
- **Estado**: Pendiente

### 2. Actor.cs - Tipo de dato IsPep

- **Archivo**: `Models/Actor.cs`
- **Linea**: 14
- **Problema**: `IsPep` esta definido como `string?`
- **Correccion**: Debe ser `bool?` segun convencion de booleanos
- **Estado**: Pendiente

### 3. Actor.cs - Campo MaritalStatus faltante

- **Archivo**: `Models/Actor.cs`
- **Problema**: No existe el campo `MaritalStatus`
- **Correccion**: Agregar campo segun requerimiento linea 60 del Modulo de Cliente
- **Estado**: Pendiente

### 4. Phone.cs - Campo VerifiedAt faltante

- **Archivo**: `Models/Phone.cs`
- **Problema**: No existe el campo `VerifiedAt`
- **Correccion**: Agregar segun requerimiento linea 53: "is_verified, verified_at"
- **Estado**: Pendiente

### 5. Phone.cs - Campo LastContactedAt faltante

- **Archivo**: `Models/Phone.cs`
- **Problema**: No existe el campo `LastContactedAt`
- **Correccion**: Agregar segun requerimiento linea 53: "last_contacted_at"
- **Estado**: Pendiente

### 6. Email.cs - Typo en FaliledAt

- **Archivo**: `Models/Email.cs`
- **Linea**: 14
- **Problema**: El campo tiene un typo `FaliledAt`
- **Correccion**: Debe ser `FailedAt`
- **Estado**: Pendiente

---

## Arquitectura

### Evaluacion del patron MVC + Repository + Service

La arquitectura actual sigue el flujo:

```
Controller -> Service -> Repository -> DbContext
```

**Conclusion**: La arquitectura es apropiada y no esta sobredimensionada.

**Justificacion**:
- Separacion de responsabilidades clara
- Facilita testing con mocks en cada capa
- Preparado para logica de negocio compleja (IA, Reglas de Negocio)
- Codigo mantenible y escalable

---

## Pendientes por Confirmar

| Item | Descripcion | Responsable |
|------|-------------|-------------|
| Tabla de notas | Se decidio no incluir por el momento (linea 66) | Equipo |
| Tabla de restricciones | Queda pendiente por definir (linea 17) | Equipo |
| Estructura de emails | Investigar Microsoft/Salesforce (linea 54) | Por asignar |

---

## Historial de Cambios

| Fecha | Descripcion |
|-------|-------------|
| 03/12/2025 | Documento creado con revision inicial de requerimientos |
