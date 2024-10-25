using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;
public class Curso
{
    public required byte IdCurso { get; set; }
    public required byte anio { get; set; }
    public required byte Division { get; set; }
    List<Alumno>Alumnos{get; set;}
    
    [SetsRequiredMembers]
    public Curso(byte anio ,byte Division, byte idcurso = 0)
    {
        this.IdCurso=IdCurso;
        this.anio=anio;
        this.Division=Division;
        this.Alumnos=new List<Alumno>();
    }

    public Curso() {}
}
