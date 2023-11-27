using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;

public class Editorial
{
    public required string Nombre { get; set; }
    public required uint IdEditorial { get; set; }
    List<Libro> Libros{get; set;}

    public Editorial()
    {
        
    }
    
    [SetsRequiredMembers]
    public Editorial (string Nombre)
    {   
        this.Nombre=Nombre;
        this.Libros=new List<Libro>();
    }
}
