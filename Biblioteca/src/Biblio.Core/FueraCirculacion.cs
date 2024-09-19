using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;

public class FueraCirculacion
{
    public required byte NumeroCopia { get; set; }
    public required ulong ISBN { get; set; }
    public DateTime FechaEgreso { get; set; }
    
    [SetsRequiredMembers]
    public FueraCirculacion(byte NumeroCopia,ulong ISBN)
    {
        this.NumeroCopia=NumeroCopia;
        this.ISBN=ISBN;
        // this.FechaSalida=FechaSalida;
    }

    public FueraCirculacion() {}
}
