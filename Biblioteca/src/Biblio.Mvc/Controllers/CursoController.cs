using Biblio.Core;
using Microsoft.AspNetCore.Mvc;
namespace Biblio.Mvc.Controllers;

public class CursoController : Controller
{
    protected readonly IAdo Ado;
    private static readonly string _cadena =
        // @"Server=localhost;Database=5to_Biblioteca;Uid=5to_agbd;pwd=Trigg3rs!;Allow User Variables=True";
        @"Server=localhost;Database=5to_Biblioteca;Uid=root;pwd=root;Allow User Variables=True";
    public CursoController()
    {
        Ado = new Biblio.AdoDapper.AdoDapper(_cadena);
    }

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
