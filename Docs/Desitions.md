Para la implementación de la solución se optó por una arquitectura de dos capas principales: una capa Web y una capa de Dominio.

La capa Web está compuesta por ASP.NET MVC, encargándose de la interacción con el usuario mediante controladores y vistas. Esta capa actúa como punto de entrada de las solicitudes y delega la ejecución de la lógica de negocio a la capa de Dominio.

La capa de Dominio fue organizada siguiendo principios de separación de responsabilidades, estructurándose de la siguiente manera:

* **Models:** contiene las entidades principales del dominio. Inicialmente se consideró la implementación de DTOs para desacoplar los modelos de persistencia de los modelos de presentación; sin embargo, debido al alcance y tiempo disponible para la prueba, se decidió simplificar la solución y trabajar directamente con las entidades.
* **Interfaces:** almacena los contratos de servicios y repositorios, permitiendo una implementación desacoplada y facilitando la mantenibilidad y las pruebas del sistema.
* **Repositories:** responsables del acceso y la persistencia de datos, encapsulando la comunicación con la base de datos y aislando esta responsabilidad del resto de la aplicación.
* **Services:** contienen la lógica de negocio y las reglas funcionales del sistema, actuando como intermediarios entre la capa Web y los repositorios.

Para la gestión de usuarios y autenticación se implementó **ASP.NET Identity** mediante **IdentityDbContext**, aprovechando la infraestructura provista por el framework para la administración de usuarios, roles, autenticación y autorización, así como la generación automática de las tablas necesarias en la base de datos.

Finalmente, el despliegue de la aplicación se realizó sobre un servidor Linux utilizando **Nginx** como servidor web y proxy inverso, permitiendo exponer la aplicación de manera segura y eficiente.
