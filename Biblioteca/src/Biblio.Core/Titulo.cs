namespace Biblio.Core;

public class Titulo
{
    public required string Nombre { get; set; }
    public required uint Publicacion { get; set; }
    public required uint IdTitulo { get; set; }
    List<Autor> Autores{get;set;}
    List<Libro> Libros{get; set;}
    public Titulo()
    {
        this.Autores=new List<Autor>();
        this.Libros=new List<Libro>();
    }
    
}
