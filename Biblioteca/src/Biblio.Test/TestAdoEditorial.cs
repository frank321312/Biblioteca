namespace Biblio.Test;

public class TestAdoEditorial:TestAdo
{
    public TestAdoEditorial():base(){}


    public void TraerEditorial(string nombre,uint idEditorial)
    {
        var editorial = Ado.ObtenerEditorial();
        Assert.Contains(editorial, a => a.Nombre == nombre );
        Assert.Contains(editorial, a => a.IdEditorial == idEditorial );

    }
}
