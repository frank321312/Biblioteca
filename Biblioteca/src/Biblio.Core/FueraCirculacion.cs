namespace Biblio.Core;

public class FueraCirculacion
{
    public required byte NumeroCopia { get; set; }

    public required ulong ISBN { get; set; }
    public required DateTime FechaSalida { get; set; }
    public FueraCirculacion(byte NumeroCopia,ulong ISBN,DateTime FechaSalida)
    {
        this.NumeroCopia=NumeroCopia;
        this.ISBN=ISBN;
        this.FechaSalida=FechaSalida;
    }
}
