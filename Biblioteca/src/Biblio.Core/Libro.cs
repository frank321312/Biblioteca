namespace Biblio.Core;
public class Libro
{
    public required Titulo Titulo { get; set; }
    public required Editorial Editorial { get; set; }
    public required ulong Isbn { get; set; }
}
