Plantilla para creación de microservicios

Descripción:
Se tienen 3 plantillas que en conjunto se usan para armar una arquitectura de 3 capas para la creación de los microservicios:
- Api.TemplateV1.1: plantilla del proyecto principal (api)
- Api.Template.CoreV1.1: plantilla para el proyecto de la capa core (librería de clases)
- Api.Template.InfraestructureV1.1: plantilla para el proyecto servicio y repositorios (librería de clases)

Prerrequisitos:
- Tener instalado visual studio community 2022
- Copiar los 3 archivos ("Api.TemplateV1.1.zip","Api.Template.CoreV1.1.zip","Api.Template.InfraestructureV1.1.zip") en C:\Users\{user}\Documents\Visual Studio 2022\Templates\ProjectTemplates

Para crear un nuevo proyecto:
1.  Iniciar visual studio 2022
2.  Seleccionar opción "Create new project"
3.  En la siguiente ventana seleccionar la plantilla "Api.TemplateV1.1" (aparece al final de la lista) -> Siguiente
4.  Asignar nombre [WebApi][ProyectoNombre]-> Crear
5.  En la solución, agregar un nuevo proyecto y seleccionar la plantilla Api.Template.CoreV1.1
6.  Asignar nombre [ProyectoNombre].[Core] -> Crear
7.  En la solución, agregar un nuevo proyecto y seleccionar la plantilla Api.Template.InfraestructureV1.1
8.  Asignar nombre [ProyectoNombre].[Infraestructure] -> Crear
9.  En el proyecto [ProyectoNombre].[Infraestructure] agregar referencia al proyecto[ProyectoNombre].[Core] y eliminar la que tiene inicialmente
10. En el proyecto principal [WebApi][ProyectoNombre], agregar referencia a los otros dos proyectos [ProyectoNombre].[Core] y [ProyectoNombre].[Infraestructure] y eliminar las dos que tiene inicialmente
11. Compilar, va a dar error por las referencias en los archivos -> actualizar las referencias -> compilar nuevamente
