using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;

public class SolicitudController:Controller
{
    protected readonly IAdo Ado;
    public SolicitudController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> ObtenerSolicitudes()
    {
        var solicitudes = await Ado.ObtenerSolicitudAsync();
        var ordersolicitudes = solicitudes.OrderBy(x => x.FechaSolicitud).ToList();
        var solicitudModal = new SolicitudModal
        {
            Solicitud=ordersolicitudes
        };
        return View("../Solicitude/Solicitudes", solicitudModal);
    }

    [HttpGet]
    public IActionResult GetAltaSolicitud()
    {
        return View("../Solicitude/DarAltaSolicitud");
    }

    [HttpPost]
    public async Task<IActionResult> AltaSolicitud(Solicitud solicitud)
    {
        await Ado.AltaSolicitudAsync(solicitud);
        return RedirectToAction(nameof(ObtenerSolicitudes));
    }
}
