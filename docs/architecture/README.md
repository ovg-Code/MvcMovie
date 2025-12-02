# Documentación de Arquitectura

Esta carpeta contiene la documentación técnica sobre la arquitectura del proyecto ARI 2.0.

## Documentos Disponibles

### 1. [Visión General de Arquitectura](./ARCHITECTURE_OVERVIEW.md)
Descripción general de la arquitectura del sistema, diagramas y decisiones de diseño.

**Lectura recomendada para:** Entender la arquitectura completa del sistema

### 2. [Repository Pattern y Service Layer](./REPOSITORY_PATTERN.md)
Explicación completa del patrón Repository y Service Layer implementado en el proyecto:
- ¿Qué es el Repository Pattern?
- ¿Qué es el Service Layer?
- Arquitectura en capas
- Interfaces vs Implementaciones
- Implementación en el proyecto
- Beneficios y mejores prácticas
- Referencias oficiales de Microsoft

**Lectura recomendada para:** Desarrolladores nuevos en el proyecto, revisión de arquitectura

---

## Principios Arquitectónicos

El proyecto ARI 2.0 sigue estos principios fundamentales:

### SOLID Principles
- **S**ingle Responsibility: Cada clase tiene una única responsabilidad
- **O**pen/Closed: Abierto a extensión, cerrado a modificación
- **L**iskov Substitution: Las implementaciones son intercambiables
- **I**nterface Segregation: Interfaces específicas y pequeñas
- **D**ependency Inversion: Dependemos de abstracciones

### Separation of Concerns
- **Presentation Layer:** Controllers + Views
- **Business Logic Layer:** Services
- **Data Access Layer:** Repositories
- **Data Layer:** Entity Framework Core + PostgreSQL

### Dependency Injection
- Uso del contenedor DI nativo de ASP.NET Core
- Registro de servicios con lifetime apropiado (Scoped)
- Inyección de dependencias vía constructor

---

## Stack Tecnológico

- **Framework:** ASP.NET Core 8.0 MVC
- **ORM:** Entity Framework Core 8.0
- **Base de Datos:** PostgreSQL 16
- **Lenguaje:** C# 12
- **Patrón:** Repository + Service Layer
- **Arquitectura:** Layered Architecture (N-Tier)

---

## Estructura del Proyecto

```
ari2.0/
├── Controllers/          # Presentation Layer
├── Services/            # Business Logic Layer
├── Repositories/        # Data Access Layer
├── Models/              # Domain Entities
├── Data/                # DbContext
├── Views/               # Razor Views
└── docs/                # Documentación
    ├── architecture/    # Documentación de arquitectura
    └── database/        # Documentación de base de datos
```

---

## Referencias

- [Documentación oficial de ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview)
- [Dependency Injection en ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
- [Repository Pattern - Microsoft](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
