using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;

public class PrestamoController:Controller
{
    protected readonly IAdo Ado;
    public PrestamoController(IAdo ado) => Ado = ado;
    
    [HttpGet]
    public async Task<IActionResult> ObtenerPrestamos()
    {
        var prestamos = await Ado.ObtenerPrestamoAsync();
        var orderprestamos = prestamos.OrderBy(x => x.FechaEgreso).ToList();
        var prestamosModal = new PrestamoModal
        {
            Prestamos=orderprestamos
        };
        return View("../Loan/Prestamos", prestamosModal);
    }

    [HttpGet]
    public IActionResult GetAltaPrestamo()
    {
        return View("../Loan/DarAltaPrestamo");
    }

    [HttpPost]
    public async Task<IActionResult> AltaSolicitud(Prestamo prestamo)
    {
        await Ado.AltaPrestamoAsync(prestamo);
        return RedirectToAction(nameof(ObtenerPrestamos));
    }

}
