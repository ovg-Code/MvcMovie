Módulo de Cliente / Registro de Cliente

14/11/2025
Notas de la reunión: Link
Definir las tecnologías a usar: Python, JavaScript
Definir la estructura de datos
Está la posibilidad de incorporar el manejo de datos biométricos como un módulo adicional en Ari 
Para la próxima reunión, todos deben traer lo que se conoce (lo que funciona y lo que no) del módulo Actor.
Crear una Prueba de Concepto (POC) para validar la metodología de documentación.

19/11/2025
Notas de la reunión: Link
Jorge Castillo elaboró un diagrama en Amazon WorkSpace con las diferentes tablas relacionadas con Cliente (Phone, Address, Gender, Type, etc).
Manejo de Notas: Se incluirá una tabla para el manejo de notas para el CRM (abarcando notas, tareas y comunicación).
Administración de Usuarios: Se definió que el usuario User no estará en la tabla Actor, ya que la administración de usuarios se realizará a través de un sistema centralizado (Autenticación de usuarios).
Campos Adicionales: Datos complementarios, como la profesión, se gestionarán utilizando un campo JSON. El uso de JSON debe limitarse a información con pocos ítems (máximo 10 a 20). Los campos repetitivos (como nacionalidad) deben permanecer en las tablas principales.
Restricciones: Se pueden manejar en una tabla adicional. Queda pendiente por definir.
Identificación: Se propone incluir otra tabla, donde un Actor puede tener varias identificaciones. UUID generado por PostgreSQL con uuid_generate_v4() (migración futura a UUIDv7 pendiente).
Auditoría: Se deben agregar los campos de auditoría a todas las tablas.
Nombrado de las tablas: Eduardo Serra va a investigar las mejores prácticas de nomenclatura para tablas.
Definición de Tecnologías: Se propuso PostgreSQL como base de datos por sus costos y performance, aunque se considera SQL Server también como un proveedor posible. En cuanto a desarrollo, se sugirió el uso de JavaScript, Node.js o C#.
Jorge Castillo va a compartir el diagrama, para que todo el equipo haga la revisión.

20/11/2025
Diagrama Customer - ARI V2

26/11/2025
Notas de la reunión: Link

Reglas genéricas de nomenclatura para todas las tablas:
Nomenclatura de las tablas:
Eliminación del prefijo: Se suprime el prefijo "TBL".
Formato: Los nombres serán en minúscula, en inglés y en plural.
Separador: Múltiples palabras se separarán con un guión bajo (_).
Ejemplo: El nombre anterior TBL_ACTOR se transforma en actors.
Campos: los campos no deben incluir el nombre de la tabla al inicio.
Ejemplo: El campo ADD_ID se transforma en id.
Tablas de tipo: el nombre debe invertirse y ser plural, usando guión bajo.
Ejemplo: La tabla TBL_TYPE_ACTOR pasa a ser actor_types.
Booleanos: todos los booleanos deben formularse como pregunta, comenzando con is_.
Ejemplos: enable pasa a ser is_enabled?, leasing a is_leased? y agent_retention a is_retention_agent?
Fechas: todas las fechas deben identificarse con _date al final.
Ejemplo: birth_date en lugar de birthday.
Marcas de tiempo: los Timestamp deben terminar en _at.
Ejemplo: updated_at en lugar de update_date.
Campos de auditoría: Toda tabla debe incluir cuatro campos de auditoría: created_by, created_at, updated_by y updated_at.

Cambios específicos / puntuales:
La tabla TBL_ADDRESS_COORDINATES se elimina y sus campos se incorporan en la tabla addresses.
Las tablas que se presten a parametrización por proyectos llevan campo JSON, para información adicional. Ejemplo: la tabla actors.
Se decidió que la nomenclatura para este campo es other_data.
Se decidió que las tablas que llevan JSON son actors y customers.
La tabla de teléfono debe incluir campos para número, extensión y tipo (móvil, trabajo, casa, oficina), además de is_verified, verified_at, y last_contacted_at.
Se asignó como tarea investigar la estructura de la tabla de emails en soluciones con trayectoria, como Microsoft o Salesforce. 
La tabla TBL_HOUSING_DEV se cambia a neighborhoods. 
Es mejor evitar los UDT(User Defined Type) para facilitar la estandarización.
El equipo acordó añadir nuevos campos en actors: 
nationality (no se va a manejar como un gentilicio, sino como país, relacionado con la tabla country)
title (como ingeniero, licenciado, etc.)
marital_status
is_pep (Persona Políticamente Expuesta)
prefix
suffix
Se debe incluir una nueva tabla para manejo de redes sociales: social_networks
La clave primaria de una tabla es simplemente id. En ese caso, se coloca el nombre en singular seguido de _id. Ejemplos: country_id y gender_id.
Por el momento, no se incluirá una tabla para el manejo de notas.

Modificar tablas según nueva nomenclatura:
Jorge Castillo modificó las tablas según lo discutido y acordado y compartió el nuevo diagrama: Diagrama de tablas de Cliente
