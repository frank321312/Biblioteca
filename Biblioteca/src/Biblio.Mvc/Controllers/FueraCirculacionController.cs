using Microsoft.AspNetCore.Mvc;
using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Http.Features;
namespace Biblio.Mvc.Controllers;

public class FueraCirculacionController : Controller
{
    protected readonly IAdo Ado;
    public FueraCirculacionController(IAdo ado) => Ado = ado;

    // private async Task<List<FueraCirculacionModal>?> DevolverFecha(List<FueraCirculacion>? fueraCirculacions)
    // {
    //     var dateCirculacion = fueraCirculacions.Select(x => new FueraCirculacionModal(x.NumeroCopia, x.ISBN, x.FechaEgreso.ToShortDateString())).ToList();
    //     return dateCirculacion;
    // }

    // private async Task<List<FueraCirculacionModal>?> DevolverFechaLibro(List<Libro>? libros)
    // {
    //     var dateCirculacion = libros.Select(x => new FueraCirculacionModal(x.FueraCirculacion.NumeroCopia, x.ISBN, x.FueraCirculacion.FechaEgreso.ToShortDateString())).ToList();
    //     return dateCirculacion;
    // }

    [HttpGet]
    public async Task<IActionResult> ObtenerFueraCirculacion()
    {
        var fueraCirculacions = await Ado.ObtenerFueraDeCirculacionAsync();
        // var dateCirculacion = fueraCirculacions.Select(x => new FueraCirculacionModal(x.NumeroCopia, x.ISBN, x.FechaEgreso.ToShortDateString())).ToList();
        var fueraDeCirculacionModal = new FueraCirculacionModal
        {
            libros = fueraCirculacions
        };
        return View("../OutCirculation/FueraCirculacion", fueraDeCirculacionModal);
    }

    [HttpGet]
    public async Task<IActionResult> GetAltaFueraCirculacion()
    {
        var libros = await Ado.ObtenerLibroAsync();
        var outCirculation = new FueraCirculacionModal();
        outCirculation.libros = libros;
        return View("../OutCirculation/AltaFueraCirculacion", outCirculation);
    }

    [HttpPost]
    public async Task<IActionResult> AltaFueraCirculacion(FueraCirculacionModal fueraCirculacion)
    {
        System.Console.WriteLine(fueraCirculacion.NumeroCopia);
        var libros = await Ado.ObtenerLibroAsync();
        var outCirculation = new FueraCirculacion(fueraCirculacion.NumeroCopia, fueraCirculacion.ISBN);
        var libro = libros.First(x => x.ISBN == fueraCirculacion.ISBN);
        await Ado.AltaFueraDeCirculacionAsync(outCirculation, libro);
        return RedirectToAction(nameof(ObtenerFueraCirculacion));
    }

    [HttpGet]
    public async Task<IActionResult> BuscarFueraCirculacion(string? busqueda)
    {
        var _fueraDeCirculacion = await Ado.ObtenerFueraDeCirculacionAsync();
        // var listFueraCirculacion = _fueraDeCirculacion.Select(x => new FueraCirculacionModal(x.NumeroCopia, x.ISBN, x.FechaEgreso.ToShortDateString())).ToList();
        var fueraDeCirculacionModal = new FueraCirculacionModal();
        if (busqueda == null)
        {
            fueraDeCirculacionModal.libros = _fueraDeCirculacion;
            return View("../Student/BusquedaAlumno", fueraDeCirculacionModal);
        }

        List<Libro>? fuera = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            fuera = await Ado.ObtenerLibroFueraDeCirculacionAsync(busqueda);
            fueraDeCirculacionModal.libros = fuera;
            if (fuera.Count() == 0)
                return View("../Student/BusquedaAlumno", fueraDeCirculacionModal);
        }
        fuera = fuera ?? new List<Libro>();
        fueraDeCirculacionModal.libros = fuera;
        return View("../Student/BusquedaAlumno", fueraDeCirculacionModal);
    }

}
