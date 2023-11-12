namespace Biblio.Core;
public class Curso
{
    public required byte IdCurso { get; set; }
    public required byte Year { get; set; }
    public required byte Division { get; set; }
    List<Alumno>Alumnos{get; set;}
    public Curso(byte Year ,byte Division)
    {
        this.Year=Year;
        this.Division=Division;
        this.Alumnos=new List<Alumno>();
    }
}
