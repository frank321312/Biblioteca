using Biblio.Core;

namespace Biblio.Test;

public class TestAdoFueraDeCirculacion:TestAdo
{
    public TestAdoFueraDeCirculacion():base(){}
    public void TraerFueraDeCirculacion(byte numeroCopia,Libro libro,DateOnly fechaSalida)
    {
        var fueraDeCirculacion = Ado.ObtenerFueraDeCirculacion();
        Assert.Contains(fueraDeCirculacion, a => a.NumeroCopia==numeroCopia);
        Assert.Contains(fueraDeCirculacion, a => a.libro==libro);
        Assert.Contains(fueraDeCirculacion, a => a.FechaSalida==fechaSalida);


    }
}
