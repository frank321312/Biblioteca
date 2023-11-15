using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;
public class Curso
{
    public required byte IdCurso { get; set; }
    public required byte año { get; set; }
    public required byte Division { get; set; }
    List<Alumno>Alumnos{get; set;}
    
    [SetsRequiredMembers]
    public Curso(byte IdCurso,byte año ,byte Division)
    {
        this.IdCurso=IdCurso;
        this.año=año;
        this.Division=Division;
        this.Alumnos=new List<Alumno>();
    }
}
