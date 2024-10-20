using Microsoft.AspNetCore.Mvc;
using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Http.Features;
namespace Biblio.Mvc.Controllers;

public class FueraCirculacionController: Controller
{
    protected readonly IAdo Ado;
    public FueraCirculacionController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> ObtenerFueraCirculacion()
    {
        var fueraCirculacions = await Ado.ObtenerFueraDeCirculacionAsync();
        var dateCirculacion = fueraCirculacions.Select(x => new FueraCirculacionModal(x.NumeroCopia, x.ISBN, x.FechaEgreso.ToShortDateString())).ToList();
        return View("../OutCirculation/FueraCirculacion", dateCirculacion);
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
        var fueraDeCirculacionModal = new FueraCirculacionModal();
        if (busqueda == null)
        {
            fueraDeCirculacionModal.fueraCirculacion = _fueraDeCirculacion;
            return View("../Student/BusquedaAlumno", fueraDeCirculacionModal);
        }
        
        IEnumerable<FueraCirculacion>? fuera = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            fuera = await Ado.BuscarFueraCirculacionAsync(busqueda);
            fueraDeCirculacionModal.fueraCirculacion = fuera.ToList();
            if (fuera.Count() == 0)
                return View("../Student/BusquedaAlumno", fueraDeCirculacionModal);
        }
        fuera = fuera ?? new List<FueraCirculacion>();
        fueraDeCirculacionModal.fueraCirculacion = fuera.ToList();
        return View("../Student/BusquedaAlumno", fueraDeCirculacionModal);
    }

}
