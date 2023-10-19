namespace Biblio.Core;

public class Prestamo
{
    public required byte NumeroCopia { get; set; }
    public required uint Dni { get; set; }
    public required ulong Isbn { get; set; }
    public required DateTime FechaEgreso { get; set; }
    public required DateTime FechaRegreso { get; set; }
}
