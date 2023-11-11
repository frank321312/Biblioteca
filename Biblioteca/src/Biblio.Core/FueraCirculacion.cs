namespace Biblio.Core;

public class FueraCirculacion
{
    public required byte NumeroCopia { get; set; }
    public required ulong ISBN { get; set; }
    public required DateOnly FechaSalida { get; set; }
}
