using Biblio.Core;

namespace Biblio.Test;
public class AdoDapperTest
{
    private readonly IAdo _ado;
    public AdoDapperTest()
    {
        var cadena = "Server=localhost;Database=5to_Biblioteca;Uid=gerenteSuper;pwd=passGerente;Allow User Variables=True";
        _ado = new Biblio.AdoDapper.AdoDapper(cadena);
    }

    [Theory]
    [InlineData("Freeman", "Eric")]
    public void TraerAutores(string apellido, string nombre)
    {
        var autores = _ado.ObtenerAutores();
        Assert.Contains(autores, a => a.Nombre == nombre && a.Apellido == apellido);
    }
}