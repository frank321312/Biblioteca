using Biblio.Core;

namespace Biblio.Test;

public class TestAdoTitulo:TestAdo
{

    public TestAdoTitulo ():base(){}

    [Fact]
    public void TraerTitulos()
    {
        var titulos=Ado.ObtenerTitulo();
        
        Assert.Contains(titulos,
                        a =>    a.titulo=="Head First Design Patterns"
                                && a.Publicacion==2004);
    }
    [Fact]
    public void AltaTitulos()
    {
        var autores = Ado.ObtenerAutores();
        Assert.NotEmpty(autores);
        
        var Guardia= new Titulo(2320,"Guardia! Guardia! Guardia!"); 
        Guardia.Autores = autores;

        Ado.AltaTitulo(Guardia);
        
        var titulo = Ado.ObtenerTitulo();
        Assert.Contains(titulo, a=> a.IdTitulo ==Guardia.IdTitulo);
    }
}
