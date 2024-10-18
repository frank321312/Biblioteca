namespace Biblio.Mvc.Controllers.Modal;

public class CursoModal
{
    public int IdCurso { get; set; }
    public required byte Anio { get; set; }
    public required byte Division { get; set; }
    public CursoModal() { }
    public CursoModal(int idCurso, byte anio, byte division)
    {
        IdCurso = idCurso;
        Anio = anio;
        Division = division;
    }
}
