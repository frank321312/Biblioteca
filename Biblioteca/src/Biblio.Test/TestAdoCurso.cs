namespace Biblio.Test;

public class TestAdoCurso:TestAdo
{
    public TestAdoCurso ():base(){}

    public void TraerCursos(byte idCurso ,byte year ,byte division)
    {
        var cursos=Ado.ObtenerCurso();
        
        Assert.Contains(cursos,a =>a.IdCurso==idCurso);
        Assert.Contains(cursos,a =>a.Year==year);
        Assert.Contains(cursos,a =>a.Division==division);
    }
}
