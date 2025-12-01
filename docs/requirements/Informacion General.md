Requerimientos ARI V2

14/11/2025
Notas de la reunión: Link
Objetivo general:
Evolucionar el sistema ARI (manteniendo su base positiva) e implementar una versión 2 con enfoque en la Inteligencia Artificial (IA) y flexibilidad de despliegue.

Funcionalidades clave:
Funcionario Virtual: automatización de la respuesta a consultas y la gestión de trámites.
Parametrización con IA: configuración del sistema mediante el uso de lenguaje natural.
Inteligencia de Negocio: Generación de análisis y reportes personalizados basados en los requerimientos específicos del cliente.

Módulos principales:
CRM (Gestión de Clientes/Contribuyentes): información personal (datos, teléfonos, direcciones, etc.), Representante Legal, Objetos.
Facturación / Pagos (Billing): Gestión de cuentas, obligaciones, estructura de pagos.
Workflow: Gestión de flujos de trabajo.

Módulos Transversales / Sub-productos:
ARI-Docs 
Reglas de Negocio
ARI Task/Forms 
IA (Pendiente de definir si será un módulo transversal o integrado)

Micro servicios (segunda etapa):
Autenticación de usuarios
Auditorías
Gestión de colas

Requerimientos:
Arquitectura:
Capacidad de funcionar en la nube (Cloud) y en infraestructura local (On-Premise).
Implementación de un Data Lake para la gestión de datos.
Enfoque en IA:
El diseño y la arquitectura deben optimizar el rendimiento de la IA.
Meta de que el 90% del código sea generado por agentes de IA de desarrollo.
Estrategia de Producto:
Cambio de nombre (manteniendo la referencia a ARI).
Definición y planificación precisa del producto.
Mantener los aspectos positivos de ARI y eliminar los negativos.
Compromiso de dedicar 1 hora semanal al proyecto (miércoles 9:00am).

Otros:
Entrega de Código Fuente: Se abordó la creciente solicitud de los clientes de recibir el código fuente. Sobre este punto, el cliente recibe el código fuente de su parametrización específica, pero no la metodología o el framework que permite la agilidad en la programación, mitigando el riesgo de entregar la totalidad de la propiedad intelectual.

