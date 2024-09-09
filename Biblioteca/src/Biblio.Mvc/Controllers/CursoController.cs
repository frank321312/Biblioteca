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
        return View("../Classroom/AltaCurso");
    }
}
