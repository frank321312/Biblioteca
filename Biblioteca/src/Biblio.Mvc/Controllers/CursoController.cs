using Biblio.Core;
using Microsoft.AspNetCore.Mvc;
namespace Biblio.Mvc.Controllers;

public class CursoController : Controller
{
    protected readonly IAdo Ado;
    public CursoController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> ObtenerCursos()
    {
        var cursos = await Ado.ObtenerCursoAsync();
        var orderCursos = cursos.OrderBy(x => x.IdCurso).ToList();
        return View("../Classroom/Curso", orderCursos);
    }

    [HttpGet]
    public IActionResult GetAltaCurso()
    {
        return View("../Classroom/AltaCurso");
    }

    [HttpPost]
    public async Task<IActionResult> AltaCurso(Curso curso)
    {
        await Ado.AltaCursoAsync(curso);
        return RedirectToAction(nameof(ObtenerCursos));
    }

    [HttpGet]
    public async Task<IActionResult> BuscarCurso(string? busqueda)
    {
        var _curso = await Ado.ObtenerCursoAsync();
        if (busqueda == null)
            return View("../Classroom/BusquedaCurso", _curso);
        IEnumerable<Curso>? curso = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            curso = await Ado.BuscarCursoAsync(busqueda);
            if (curso.Count() == 0)
                return View("../Classroom/Curso");
        }
        curso = curso ?? new List<Curso>();
        return View("../Classroom/BusquedaCurso", curso);
    }
    [HttpGet]
    public async Task<IActionResult> DetalleDeAlumnos(int Curso)
    {
        var alumnos = await Ado.ObtenerAlumnosAsync();
        var cursoAlumo=alumnos.Where(x=>x.IdCurso== Curso).ToList();
        var cursos = await Ado.ObtenerCursoAsync();
        var curso = cursos.Find(x => x.IdCurso == Curso);
        var orderAlumno = cursoAlumo.OrderBy(x => x.Dni).ToList();
        var alumnoModal = new AlumnoModal
        {
            alumnos = orderAlumno,
            Anio = curso.anio,
            Division = curso.Division
        };
        return View("../Student/BusquedaAlumno", alumnoModal);
    }
}
