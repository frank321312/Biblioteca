using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;

public class LibroController : Controller
{
    protected readonly IAdo Ado;
    public LibroController(IAdo ado) => Ado = ado;
    
    [HttpGet]
    public async Task<IActionResult> ObtenerLibro()
    {
        var libro = await Ado.ObtenerLibroAsync();
        var ordenarlibro = libro.OrderBy(x => x.ISBN).ToList();
        var select = ordenarlibro.Select(x => x.Titulo).ToList();
        return View("../Book/Libro", select);
    }
}
