using Biblio.Core;

namespace Biblio.Test;

public class TestAdoAlumno : TestAdo
{
    public TestAdoAlumno() : base() { }
    [Theory]
    [InlineData(1231564,"roque", "rivas",7,12231534,"jose@gmail.com",10)]

    public void TraerAlumnos(uint dni, string nombre, string apellido, byte curso, uint celular
    , string email, byte IdCurso )
    {
        var alumnos = Ado.ObtenerAlumnos();
        Assert.Contains(alumnos, a => a.Dni == dni);
        Assert.Contains(alumnos, a => a.Nombre == nombre);
        Assert.Contains(alumnos, a => a.Apellido == apellido);
        Assert.Contains(alumnos, a => a.Curso == curso);
        Assert.Contains(alumnos, a => a.Celular == celular);
        Assert.Contains(alumnos, a => a.Email == email);
        Assert.Contains(alumnos, a => a.IdCurso == IdCurso);
    }
    [Fact]
    public void AltaAlumnoTest()
    {
        uint dni = 1231564;
        var roque = new Alumno(dni,"roque", "rivas",7,12231534,"jose@gmail.com",10);

        var alumnos = Ado.ObtenerAlumnos();
        Assert.DoesNotContain(alumnos, a=> a.Dni == dni);

        Ado.AltaAlumno(roque, "pass123");        
        
        alumnos = Ado.ObtenerAlumnos();
        Assert.Contains(alumnos, a=> a.Dni == dni);
    }
}
