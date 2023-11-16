using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;

public class Titulo
{
    public required string titulo { get; set; }
    public required ushort Publicacion { get; set; }
    public required uint IdTitulo { get; set; }
    public List<Autor> Autores{get;set;}
    List<Libro> Libros{get; set;}
    
    [SetsRequiredMembers]
    public Titulo(ushort Publicacion, string titulo)
    {
        this.titulo=titulo;
        this.Publicacion=Publicacion;
        this.IdTitulo=IdTitulo;
        Autores=new List<Autor>();
        Libros=new List<Libro>();
    }
    
}
