using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;

public class Alumno
{
    public required uint Dni { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }

    public required uint Celular { get; set; }
    public required string Email { get; set; }
    public required byte IdCurso { get; set; }
    List<Solicitud> Solicitudes { get; set; }
    List<Prestamo> Prestamos { get; set; }
    public Alumno()
    {
        
    }

    [SetsRequiredMembers]
    public Alumno(string Nombre, string Apellido, uint Celular, string Email, uint Dni, byte IdCurso)
    {
        this.Dni = Dni;
        this.Nombre = Nombre;
        this.Apellido = Apellido;
        this.Celular = Celular;
        this.Email = Email;
        this.IdCurso = IdCurso;
        this.Solicitudes = new List<Solicitud>();
        this.Prestamos = new List<Prestamo>();
    }
    // test
}
