using Biblio.Core;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;

public class AlumnoController: Controller
{
    protected readonly IAdo Ado;
    private static readonly string _cadena =
        @"Server=localhost;Database=5to_Biblioteca;Uid=5to_agbd;pwd=Trigg3rs!;Allow User Variables=True";
    public AlumnoController()
    {
        Ado = new Biblio.AdoDapper.AdoDapper(_cadena);
    }   
    [HttpGet]
    public async Task<IActionResult> ObtenerAlumnos()
    {
        var alumnos = await Ado.ObtenerAlumnosAsync();
        var orderAlumno = alumnos.OrderBy(x => x.Dni).ToList();
        return View("../student/Alumno", orderAlumno);
    }
    [HttpPost]
    public async Task<IActionResult> altaAlumno(Alumno alumno,string pass)
    {
        await Ado.AltaAlumnoAsync(alumno,pass);
        // return RedirectToAction(nameof(ObtenerAutores));
        return View("../Author/AltaAutor");
    }
}
