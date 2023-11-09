namespace Biblio.Core;

public class Titulo
{
    public required string Nombre { get; set; }
    public required uint Publicacion { get; set; }
    public required uint IdTitulo { get; set; }
    List<Autor> Autores{get;set;}
    public Titulo()
    {
        this.Autores=new List<Autor>();
    }
    
}
