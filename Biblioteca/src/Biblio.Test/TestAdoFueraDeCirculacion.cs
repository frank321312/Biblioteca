using Biblio.Core;

namespace Biblio.Test;

public class TestAdoFueraDeCirculacion:TestAdo
{
    public TestAdoFueraDeCirculacion():base(){}
    public void TraerFueraDeCirculacion(byte numeroCopia,ulong ISBN)
    {
        var fueraDeCirculacion = Ado.ObtenerFueraDeCirculacion();
        Assert.Contains(fueraDeCirculacion, a => a.NumeroCopia==numeroCopia);
        Assert.Contains(fueraDeCirculacion, a => a.ISBN==ISBN);
    }
}
