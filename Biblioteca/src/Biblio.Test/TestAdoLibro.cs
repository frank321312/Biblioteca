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

    [Fact]
    public void AltaLibroTest()
    {
        var editorial = Ado.ObtenerEditorial().First(e => e.IdEditorial == 1);
        Assert.NotNull(editorial);

        var titulo = Ado.ObtenerTitulo().First(t => t.IdTitulo == 1);
        Assert.NotNull(titulo);

        var libro_1 = new Libro(titulo, editorial, 321234);
        Ado.AltaLibro(libro_1);
    }
}
