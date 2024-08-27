using Biblio.Core;
namespace Biblio.Mvc.Controllers;

public class AlumnoModal
{
    public required uint Dni { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }

    public required uint Celular { get; set; }
    public required string Email { get; set; }
    public required byte IdCurso { get; set; }
    public List<Solicitud>? Solicitudes { get; set; }
    public List<Prestamo>? Prestamos { get; set; }
    public List<Alumno>? alumnos;
    
    public AlumnoModal() {}
    public void SetAlumnos(List<Alumno> students)
        => alumnos = students;
}
