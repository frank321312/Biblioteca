namespace Biblio.Test;

public class TestAdoEditorial:TestAdo
{
    [Theory]
    [InlineData("O REILLY")]
    public void TraerEditorial(string nombre)
    {
        var editorial = Ado.ObtenerEditorial();
        Assert.Contains(editorial, a => a.Nombre == nombre );
    }
}