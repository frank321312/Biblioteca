using Biblio.Core;

namespace Biblio.Test;

public class TestAdoCurso:TestAdo
{
    public TestAdoCurso ():base(){}
    [Theory]
    [InlineData(12,8,5)]
    
    public void TraerCursos(byte idCurso ,byte year ,byte division)
    {
        var cursos=Ado.ObtenerCurso();
        
        Assert.Contains(cursos,a =>a.IdCurso==idCurso);
        Assert.Contains(cursos,a =>a.Year==year);
        Assert.Contains(cursos,a =>a.Division==division);
    }
    [Fact]
    public void AltaCurso()
    {
        byte idcurso=12;
        var curso8 = new Curso(idcurso,8,5);
        
        var curso =Ado.ObtenerCurso();
        Assert.DoesNotContain(curso, a => a.IdCurso == idcurso);
        
        Ado.AltaCurso(curso8);
        curso = Ado.ObtenerCurso();
        Assert.Contains(curso, a=> a.IdCurso == idcurso);
    }
    
}
