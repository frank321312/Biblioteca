using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;
public class Curso
{
    public required byte IdCurso { get; set; }
    public required byte Year { get; set; }
    public required byte Division { get; set; }
    List<Alumno>Alumnos{get; set;}
    [SetsRequiredMembers]
    public Curso(byte IdCurso,byte Year ,byte Division)
    {
        this.IdCurso=IdCurso;
        this.Year=Year;
        this.Division=Division;
        this.Alumnos=new List<Alumno>();
    }
}
