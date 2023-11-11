namespace Biblio.Core;

public class Editorial
{
    public required string Nombre { get; set; }
    public required uint IdEditorial { get; set; }
    List<Libro> Libros{get; set;}
    public Editorial ()
    {   
        this.Libros=new List<Libro>();
    }
}
