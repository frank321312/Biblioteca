namespace Biblio.Core;
public class Curso
{
    public required byte IdCurso { get; set; }
    public required byte Year { get; set; }
    public required byte Division { get; set; }
    List<Alumno>Alumnos{get; set;}
    public Curso()
    {
        this.Alumnos=new List<Alumno>();
    }
}
