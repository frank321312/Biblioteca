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
    public async Task<IActionResult> DetalleDeAlumnos(int IdCurso)
    {
        var cursos = await Ado.ObtenerCursoAsync();
        var orderCursos = cursos.OrderBy(x => x.IdCurso).ToList();

        if (IdCurso == null)
        {
            return View("../Classroom/Curso", orderCursos);
        }

        var alumnos = await Ado.ObtenerAlumnosDelCursoAsync(IdCurso);
        var orderAlumno = alumnos.OrderBy(x => x.Dni).ToList();

        var alumnoModal = new AlumnoModal
        {
            alumnos = orderAlumno,
        };
        return View("../Classroom/CursoDetalle", alumnoModal);
    }
}
