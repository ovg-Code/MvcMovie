# Historial de Scaffolding

Documentación de todos los comandos de scaffolding ejecutados en el proyecto.

## Comandos Base

### Scaffolding Individual
```bash
dotnet aspnet-codegenerator controller \
  -name [Controller]Controller \
  -m [Model] \
  -dc ApplicationDbContext \
  --relativeFolderPath Controllers \
  --useDefaultLayout \
  --referenceScriptLibraries \
  --databaseProvider postgres
```

## Módulo Cliente - 2025-12-01

### Catálogos Base (Primera Fase)
```bash
# Countries
dotnet aspnet-codegenerator controller -name CountriesController -m Country -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# Genders
dotnet aspnet-codegenerator controller -name GendersController -m Gender -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# ActorTypes
dotnet aspnet-codegenerator controller -name ActorTypesController -m ActorType -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# PhoneTypes
dotnet aspnet-codegenerator controller -name PhoneTypesController -m PhoneType -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# AddressTypes
dotnet aspnet-codegenerator controller -name AddressTypesController -m AddressType -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# States
dotnet aspnet-codegenerator controller -name StatesController -m State -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# Cities
dotnet aspnet-codegenerator controller -name CitiesController -m City -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# Municipalities
dotnet aspnet-codegenerator controller -name MunicipalitiesController -m Municipality -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# Neighborhoods
dotnet aspnet-codegenerator controller -name NeighborhoodsController -m Neighborhood -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# ZipCodes
dotnet aspnet-codegenerator controller -name ZipCodesController -m ZipCode -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres
```

### Tablas Principales (Segunda Fase)
```bash
# Actor
dotnet aspnet-codegenerator controller -name ActorsController -m Actor -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# Customer
dotnet aspnet-codegenerator controller -name CustomersController -m Customer -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# Phone
dotnet aspnet-codegenerator controller -name PhonesController -m Phone -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# Email
dotnet aspnet-codegenerator controller -name EmailsController -m Email -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# Address
dotnet aspnet-codegenerator controller -name AddresssController -m Address -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# IdentityCard
dotnet aspnet-codegenerator controller -name IdentityCardsController -m IdentityCard -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# IdentityCardType
dotnet aspnet-codegenerator controller -name IdentityCardTypesController -m IdentityCardType -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# ActorRelationship
dotnet aspnet-codegenerator controller -name ActorRelationshipsController -m ActorRelationship -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# RelationshipType
dotnet aspnet-codegenerator controller -name RelationshipTypesController -m RelationshipType -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# SocialNetwork
dotnet aspnet-codegenerator controller -name SocialNetworksController -m SocialNetwork -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres

# CustomerPublicStatusType
dotnet aspnet-codegenerator controller -name CustomerPublicStatusTypesController -m CustomerPublicStatusType -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider postgres
```

## Resumen
- **Total tablas**: 21
- **Total controladores**: 21
- **Total vistas**: 105 (5 por controlador: Index, Create, Edit, Details, Delete)
- **Tiempo estimado**: ~5 minutos vs 8+ horas manual
- **Herramienta**: dotnet-aspnet-codegenerator 9.0.0
- **Base de datos**: PostgreSQL con EF Core 9.0

## Scripts Automatizados
Ver `/scripts/generate_crud.sh` y `/scripts/generate_new_crud.sh` para generación batch.

## Notas
- Todos los modelos usan UUID v7 como PK
- Nomenclatura snake_case en BD vía EFCore.NamingConventions
- Scaffolding oficial de Microsoft (no custom)
