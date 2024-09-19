using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;

public class Titulo
{
    public required string nombre { get; set; }
    public required ushort Publicacion { get; set; }
    public required uint IdTitulo { get; set; }
    public List<Autor> Autores{get;set;}
    public List<Libro> Libros {get; set;}

    public Titulo() {}
    
    [SetsRequiredMembers]
    public Titulo(ushort Publicacion, string titulo, uint IdTitulo = 0)
    {
        this.nombre=titulo;
        this.Publicacion=Publicacion;
        this.IdTitulo=IdTitulo;
        Autores=new List<Autor>();
        Libros=new List<Libro>();
    }
}
