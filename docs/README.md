# Documentación ARIV2

Sistema CRM con ASP.NET Core MVC + PostgreSQL + EF Core 9.0

**Endpoint:** ariv2-crm-db.curms68ogomm.us-east-1.rds.amazonaws.com  
**Base de datos:** ariv2  
**Total de tablas:** 21

---

## 📁 Estructura de Documentación

### 📋 [requirements/](requirements/)
Requerimientos funcionales y de negocio
- **Informacion General.md** - Visión general del sistema
- **Modulo de Cliente.md** - Especificación del módulo de clientes

### 🗄️ [database/](database/)
Documentación de todas las tablas de la base de datos (21 archivos)

**Actores y Relaciones:**
- actors.md, actor_types.md, actor_relationships.md, relationship_types.md

**Clientes:**
- customers.md, customer_public_status_types.md

**Información Personal:**
- genders.md, identity_cards.md, identity_card_types.md

**Contacto:**
- emails.md, phones.md, phone_types.md, social_networks.md

**Direcciones:**
- addresses.md, address_types.md, countries.md, states.md, municipalities.md, cities.md, neighborhoods.md, zip_codes.md

### 🛠️ [development/](development/)
Guías técnicas de desarrollo

- **SCAFFOLDING_HISTORY.md** - Historial de comandos ejecutados
- **SCAFFOLDING_AUTOMATICO.md** - Proceso de scaffolding automático
- **TRADUCTOR_NOMENCLATURA.md** - Configuración snake_case (EFCore.NamingConventions)

---

## 🚀 Scripts

Ver carpeta `/scripts/` en la raíz del proyecto:
- `generate_crud.sh` - Generación batch de controladores
- `generate_new_crud.sh` - Script mejorado para nuevos controladores

---

## 📐 Convenciones

### Base de Datos
- **id**: UUID v7 (generación automática con UUIDNext)
- **created_at/updated_at**: Timestamps automáticos
- **created_by/updated_by**: Usuario que creó/modificó
- **is_enabled**: Soft delete flag
- **other_data**: JSONB para datos flexibles

### Código
- **Nomenclatura BD**: snake_case (vía EFCore.NamingConventions)
- **Nomenclatura C#**: PascalCase (estándar .NET)
- **Framework**: ASP.NET Core MVC 8.0
- **ORM**: Entity Framework Core 9.0
- **Base de datos**: PostgreSQL (Npgsql 9.0.2)

---

## 📊 Estadísticas del Proyecto

- **Modelos**: 21
- **Controladores**: 21
- **Vistas**: 105 (5 por controlador)
- **Tiempo de scaffolding**: ~5 minutos (vs 8+ horas manual)
