<h1 align="center">E.T. Nº12 D.E. 1º "Libertador Gral. José de San Martín"</h1>
<p align="center">
  <img src="https://et12.edu.ar/imgs/et12.svg">
</p>

## Computación 2024

**Asignatura**: **Programación Sobre Redes**

**Nombre TP**: **HospedApp**

**Apellido y nombre Alumno**: **Diego Vega, Josbert Rizzo, Hector Sacaca**

**Curso**: **6to 7ma**

# Biblioteca MVC

**Biblioteca MVC** es un sitio web diseñado para facilitar el seguimiento de los libros prestados a los alumnos. Permite registrar y acceder fácilmente a información clave como el nombre del estudiante, su curso y los detalles del libro. Esto agiliza la gestión del préstamo, asegurando que se mantenga un control efectivo y se minimicen las pérdidas.

## Funcionalidades principales:

- **Gestión de Prestamos de los libros**: Registra y administra los diferentes libros que se dieron en prestamo y mantiene un seguimiento de aquellos que estan fuera de circulacion.
- **Informe de Alumnos**: Nos permite mostrar la informacion necesaria de aquellos alumnos que solicitaron un prestamo de algun libro.
- **Libros**: 
- **Cursos**: 

## Comenzando 🚀

```
git clone https://github.com/frank321312/Biblioteca.git
```

### Pre-requisitos 📋

Antes de comenzar, asegúrate de tener instalados los siguientes componentes en tu máquina:

1. **[.NET SDK](https://dotnet.microsoft.com/download)** - Necesario para compilar y ejecutar aplicaciones en C#.
2. **[MySQL](https://www.mysql.com/downloads/)** - Debes tener MySQL instalado y en funcionamiento para gestionar la base de datos.
5. **[Git](https://git-scm.com/)** - Para clonar el repositorio y gestionar el control de versiones.

## Construido con 🛠️

- **[Visual Studio Code](https://code.visualstudio.com/)** - Editor de código ligero utilizado para el desarrollo del proyecto.
- **[C#](https://learn.microsoft.com/dotnet/csharp/)** - Lenguaje de programación utilizado para la lógica de la aplicación.
- **[ASP.NET MVC](https://dotnet.microsoft.com/apps/aspnet/mvc)** - Framework para estructurar la aplicación siguiendo el patrón Modelo-Vista-Controlador (MVC).
- **[MySQL](https://www.mysql.com/)** - Base de datos relacional para almacenar la información.
- **[Dapper](https://github.com/DapperLib/Dapper)** - Micro ORM utilizado para facilitar el acceso a la base de datos.
- **[Bootstrap](https://getbootstrap.com/)** - Framework CSS utilizado para dar estilo y estructura a la interfaz.

## Autores ✒️

* **Diego Vega** - *Desarrollador Front-end* - [Diego-VJM](https://github.com/Diego-VJM)
* **Hector Sacaca** - *Desarrollador Full-Stack* - [frank321312](https://github.com/frank321312)
* **Josbert Rizzo** - *Desarrollador Back-end* - [JRyoy](https://github.com/JRyoy)



## Desventajas del proyecto
### 1. Uso masivo de memoria:
El proyecto en si al consultar la base de datos, al consultar una tabla, no trae en si en un dato especifico, sino que trae todos los datos de la tabla, en caso de una relacion traerá también en esa tabla, lo que termina causando que el navegador del usuario utilice más recursos.
### 2. El formato de entrada falta de validaciones: 
Durante el proceso de registro de datos del usuario no se realiza correctamente o parcialmente la validación de los campos de datos adecuados, lo que puede generar discrepancias o errores en la captura de datos.