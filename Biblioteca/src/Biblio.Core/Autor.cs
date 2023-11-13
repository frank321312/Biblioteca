using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;

public class Autor
{
    public ushort IdAutor { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    
    [SetsRequiredMembers]
    public Autor (string Nombre ,string Apellido)
    {
        this.Nombre=Nombre;
        this.Apellido=Apellido;
    }
}
