using Biblio.Core;

namespace Biblio.Test;

public class TestAdoAlumno:TestAdo
{
    public TestAdoAlumno():base(){}
    public void TraerAlumnos(uint dni,string nombre,string apellido ,byte curso ,uint celular 
    ,string email ,string contraseña,Curso idCurso )
    {
        var alumnos = Ado.ObtenerAlumnos();
        Assert.Contains(alumnos, a => a.Dni== dni);
        Assert.Contains(alumnos, a => a.Nombre == nombre );
        Assert.Contains(alumnos, a => a.Apellido== apellido);
        Assert.Contains(alumnos, a => a.Curso==curso );
        Assert.Contains(alumnos, a => a.Celular== celular);
        Assert.Contains(alumnos, a => a.Email==email);
        Assert.Contains(alumnos, a => a.Contraseña==contraseña );
        Assert.Contains(alumnos, a => a.IdCurso==idCurso);

    }
}
