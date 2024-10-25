using Biblio.Core;

namespace Biblio.Test;

public class TestAdoAlumno : TestAdo
{
    public TestAdoAlumno() : base() { }
    [Theory]
    [InlineData(48186408, "Pepito", "Perez", 1125648696, "pepito11@gmail.com", 1)]

    public void TraerAlumnos(uint dni, string nombre, string apellido, uint celular
    , string email, byte IdCurso)
    {
        var alumnos = Ado.ObtenerAlumnos();
        Assert.Contains(alumnos, a => a.Dni == dni);
        Assert.Contains(alumnos, a => a.Nombre == nombre);
        Assert.Contains(alumnos, a => a.Apellido == apellido);
        Assert.Contains(alumnos, a => a.Celular == celular);
        Assert.Contains(alumnos, a => a.Email == email);
        Assert.Contains(alumnos, a => a.IdCurso == IdCurso);
    }
    [Fact]
    public void AltaAlumnoTest()
    {
        uint dni = 1231564;
        var roque = new Alumno("roque", "rivas", 12231534, "jose@gmail.com", dni, 1);

        var alumnos = Ado.ObtenerAlumnos();
        Assert.DoesNotContain(alumnos, a => a.Dni == dni);

        Ado.AltaAlumno(roque, "pass123");

        alumnos = Ado.ObtenerAlumnos();
        Assert.Contains(alumnos, a => a.Dni == dni);
    }
}
