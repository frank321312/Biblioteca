using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;

public class Titulo
{
    public required string Nombre { get; set; }
    public required uint Publicacion { get; set; }
    public required uint IdTitulo { get; set; }
    List<Autor> Autores{get;set;}
    List<Libro> Libros{get; set;}
    [SetsRequiredMembers]
    public Titulo(string Nombre,uint Publicacion,uint IdTitulo)
    {
        this.Nombre=Nombre;
        this.Publicacion=Publicacion;
        this.IdTitulo=IdTitulo;
        Autores=new List<Autor>();
        Libros=new List<Libro>();
    }
    
}
