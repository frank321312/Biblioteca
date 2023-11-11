namespace Biblio.Core;

public class Autor
{
    public required ushort IdAutor { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    List<Titulo>Titulos{get; set;}
    public Autor ()
    {
        this.Titulos=new List<Titulo>();
    }
}
