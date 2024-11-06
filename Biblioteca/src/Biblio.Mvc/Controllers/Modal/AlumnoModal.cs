using Biblio.Core;
namespace Biblio.Mvc.Controllers;

public class AlumnoModal
{
    public uint Dni { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public uint Celular { get; set; }
    public string? Email { get; set; }
    public string? Pass { get; set; }
    public byte idCurso { get; set; }
    public byte Anio { get; set; }
    public  byte Division { get; set; }
    public Alumno? alumno { get; set; }
    public Curso? curso { get; set; }
    public List<Curso>? cursos { get; set; }
    public List<Alumno> alumnos { get; set; }
    public List<Prestamo> Prestamos{ get; set; }
    public string busqueda { get; set; } = string.Empty;
    // public List<Solicitud>? Solicitudes { get; set; }
    // public List<Prestamo>? Prestamos { get; set; }
    // public List<Alumno>? alumnos;
    
    // public AlumnoModal() {}
    public void SetCursos(List<Curso> cursos)
        => this.cursos = cursos;
}
