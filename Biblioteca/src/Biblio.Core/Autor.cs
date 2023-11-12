namespace Biblio.Core;

public class Autor
{
    public required ushort IdAutor { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    List<Titulo>Titulos{get; set;}
    public Autor (string Nombre ,string Apellido)
    {
        this.Nombre=Nombre;
        this.Apellido=Apellido;
        this.Titulos=new List<Titulo>();
    }
}
