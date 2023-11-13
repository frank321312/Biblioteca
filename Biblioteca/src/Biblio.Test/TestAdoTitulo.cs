namespace Biblio.Test;

public class TestAdoTitulo:TestAdo
{
     public TestAdoTitulo ():base(){}
    
    public void TraerTitulos(string nombre, uint publicacion,uint idTitulo)
    {
        var titulos=Ado.ObtenrTitulo();
        
        Assert.Contains(titulos,a =>a.Nombre==nombre);
        Assert.Contains(titulos,a =>a.Publicacion==publicacion);
        Assert.Contains(titulos,a =>a.IdTitulo==idTitulo);
    }
}
