using Biblio.Core;

namespace Biblio.Test;
public class TestAdoAutor : TestAdo
{
    public TestAdoAutor() : base() {}

    [Theory]
    [InlineData("Freeman", "Eric")]
    public void TraerAutores(string apellido, string nombre)
    {
        var autores = Ado.ObtenerAutores();
        Assert.Contains(autores, a => a.Nombre == nombre && a.Apellido == apellido);
    }

    [Fact]
    public void AltaAutorTest()
    {
        var facundoQ = new Autor("Facundo", "Quiroga");

        Assert.Equal(0, facundoQ.IdAutor);

        Ado.AltaAutor(facundoQ);
        
        Assert.NotEqual(0, facundoQ.IdAutor);
    }
    
}