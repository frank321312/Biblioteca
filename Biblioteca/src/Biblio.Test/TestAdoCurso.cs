using Biblio.Core;

namespace Biblio.Test;

public class TestAdoCurso:TestAdo
{
    public TestAdoCurso ():base(){}
    [Theory]
    [InlineData(5,7)]
    
    public void TraerCursos(byte anio ,byte division)
    {
        var cursos=Ado.ObtenerCurso();
        Assert.Contains(cursos,a =>a.anio==anio && a.Division==division);        
    }
    [Fact]
    public void AltaCurso()
    {
        var curso8 = new Curso(8,5);
        
        var curso =Ado.ObtenerCurso();
        Assert.DoesNotContain(curso, a =>a.anio == 8 && a.Division == 5  );
        
        Ado.AltaCurso(curso8);
        curso = Ado.ObtenerCurso();
        Assert.Contains(curso, a=> a.anio == 8 && a.Division == 5  );
    }
    
}
