using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;

public class PrestamoController : Controller
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
            Prestamos = orderprestamos
        };
        return View("../Loan/Prestamos", prestamosModal);
    }

    [HttpGet]
    public IActionResult GetAltaPrestamo()
    {
        return View("../Loan/DarAltaPrestamo");
    }

    [HttpPost]
    public async Task<IActionResult> AltaPrestamo(PrestamoModal prestamoModal)
    {
        var prestamo = new Prestamo {
            Dni=prestamoModal.Dni,
            NumeroCopia=prestamoModal.NumeroCopia,
            ISBN=prestamoModal.ISBN,
            FechaEgreso= DateTime.Now,
            FechaRegreso=null,
        };
        await Ado.AltaPrestamoAsync(prestamo);
        return RedirectToAction(nameof(ObtenerPrestamos));
    }
    
    [HttpGet]
    public async Task<IActionResult> VerPrestamos(string cadena)
    {
        var lista = cadena.Split(",");
        var dni = Convert.ToUInt32(lista[0]);
        var isbn = Convert.ToUInt32(lista[1]);
        var prestamos = await Ado.ObtenerPrestamoAsync();
        var PrestamoAlumo = prestamos.Where(x => x.Dni == dni).ToList();
        var alumnos = await Ado.ObtenerAlumnosAsync();
        var alumno = alumnos.Find(x => x.Dni == dni);

        var titulo = await Ado.ObtenerLibroPorISBNAsync(isbn);
        var orderprestamos = PrestamoAlumo.OrderBy(x => x.Dni).ToList();
        var alumnoModal = new AlumnoModal
        {
            Prestamos = orderprestamos,
            alumnos = alumnos,
            Nombre = titulo.Titulo.nombre
        };
        return View("../Loan/DetallePrestamos", alumnoModal);
    }
    [HttpPut]
    public async Task<IActionResult> FinalizarPrestamo(string cadena)
    {
        var lista = cadena.Split(",");
        var isbn = Convert.ToUInt32(lista[1]);
        var numeroCopia = Convert.ToUInt32(lista[0]);
        await Ado.ActualizarFechaPrestamo(isbn,numeroCopia);
        var prestamo = new AlumnoModal
        {
            
        };
        return View("../Loan/DetallePrestamos",prestamo);
    }
}
