using System.IO.Compression;
using Biblio.Core;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;

public class AlumnoController : Controller
{
    protected readonly IAdo Ado;
    public AlumnoController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> ObtenerAlumnos()
    {
        var alumnos = await Ado.ObtenerAlumnosAsync();
        var orderAlumno = alumnos.OrderBy(x => x.Dni).ToList();
        var alumnoModal = new AlumnoModal
        {
            alumnos = orderAlumno
        };
        return View("../Student/BusquedaAlumno", alumnoModal);
    }

    [HttpGet]
    public async Task<IActionResult> GetAltaAlumno()
    {
        var cursos = await Ado.ObtenerCursoAsync();
        var orderCursos = cursos.OrderBy(x => x.IdCurso).ToList();
        AlumnoModal alumnoModal = new AlumnoModal();
        alumnoModal.SetCursos(orderCursos);
        return View("../Student/AltaAlumno", alumnoModal);
    }

    [HttpPost]
    public async Task<IActionResult> AltaAlumno(Alumno alumno, string pass)
    {
        try
        {
            await Ado.AltaAlumnoAsync(alumno, pass);
        }
        catch (System.Exception e)
        {
            if (e.Message.Contains("Duplicate entry"))
            {
                RedirectToAction(nameof(GetAltaAlumno));
            }
            throw;
        }
        return RedirectToAction(nameof(ObtenerAlumnos));
    }

    [HttpGet]
    public async Task<IActionResult> BuscarAlumno(string? busqueda)
    {
        var _alumno = await Ado.ObtenerAlumnosAsync();
        var alumnoModal = new AlumnoModal();
        if (busqueda == null)
        {
            alumnoModal.alumnos = _alumno;
            return View("../Student/BusquedaAlumno", alumnoModal);
        }

        IEnumerable<Alumno>? alumno = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            alumno = await Ado.BuscarAlumnoAsync(busqueda);
            alumnoModal.alumnos = alumno.ToList();
            if (alumno.Count() == 0)
                return View("../Student/BusquedaAlumno", alumnoModal);
        }
        alumno = alumno ?? new List<Alumno>();
        alumnoModal.alumnos = alumno.ToList();
        return View("../Student/BusquedaAlumno", alumnoModal);
    }
}