namespace Biblio.Core;

public class Alumno
{
    public required uint Dni { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required byte Curso { get; set; }
    public required uint Celular { get; set; }
    public required string Email { get; set; }
    public required string Contraseña { get; set; }
    public required Curso IdCurso { get; set; }
}
