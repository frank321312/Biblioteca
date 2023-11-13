using Biblio.Core;

namespace Biblio.Test;

public class TestAdoLibro:TestAdo
{
    public TestAdoLibro():base(){}

    public void TraerLibros(Titulo titulo , Editorial editorial,ulong iSBN)
    {
        var Libros=Ado.ObtenerLibro();
        Assert.Contains(Libros, a => a.Titulo== titulo);
        Assert.Contains(Libros, a => a.Editorial==editorial);
        Assert.Contains(Libros, a => a.ISBN==iSBN);
    }
}
