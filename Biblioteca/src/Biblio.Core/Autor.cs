using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;

public class Autor
{
    public ushort IdAutor { get; set; }
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public Autor() {}

    [SetsRequiredMembers]
    public Autor(string Nombre, string Apellido)
    {
        this.Nombre = Nombre;
        this.Apellido = Apellido;
    }
    [SetsRequiredMembers]
    public Autor(string Nombre, string Apellido, ushort idAutor) : this(Nombre, Apellido)
        => IdAutor = idAutor;
    public override string ToString()
        => $"{Apellido}, {Nombre}";
}