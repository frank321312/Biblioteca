using Biblio.Core;

namespace Biblio.Test;
public class DapperAutorTest : TestAdo
{
    public DapperAutorTest() : base() {}

    [Theory]
    [InlineData("Freeman", "Eric")]
    public void TraerAutores(string apellido, string nombre)
    {
        var autores = Ado.ObtenerAutores();
        Assert.Contains(autores, a => a.Nombre == nombre && a.Apellido == apellido);
    }
    
}