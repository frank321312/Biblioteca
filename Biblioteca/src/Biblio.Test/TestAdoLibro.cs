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
        ulong ISBN = 321234;
        var editorial = Ado.ObtenerEditorial().First(e => e.IdEditorial == 1);
        Assert.NotNull(editorial);

        var titulo = Ado.ObtenerTitulo().First(t => t.IdTitulo == 1);
        Assert.NotNull(titulo);

        var libro1 = new Libro(titulo, editorial, ISBN);
        Ado.AltaLibro(libro1);

        var mismoLibro = Ado.ObtenerLibroPorISBN(321234);
        Assert.NotNull(mismoLibro);

        var mismoAutor = Ado.ObtenerAutorPorISBN(321234);
        Assert.NotNull(mismoAutor);
    }

    [Fact]
    public void DetalleLibro()
    {
        Ado.ObtenerLibroPorISBN(596007124);
    }
}

	// -- INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial
	// -- INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
	// -- INNER JOIN AutorTitulo ON Titulo.idTitulo = AutorTitulo.idTitulo
	// -- INNER JOIN Autor ON AutorTitulo.idAutor = Autor.idAutor
