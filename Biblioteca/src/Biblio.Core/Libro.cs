namespace Biblio.Core;

public class Libro
{
    public required Titulo IdTitulo { get; set; }
    public required Editorial IdEditorial { get; set; }
    public required ulong Isbn { get; set; }
}
