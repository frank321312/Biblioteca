using Biblio.Core;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;
public class AutorController : Controller
{
    protected readonly IAdo Ado;
    public AutorController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> ObtenerAutores()
    {
        var autores = await Ado.ObtenerAutoresAsync();
        var orderAutores = autores.OrderBy(x => x.IdAutor).ToList();
        return View("../Author/Autor", orderAutores);
    }

    [HttpGet]
    public IActionResult GetAltaAutor()
    {
        return View("../Author/AltaAutor");
    }

    [HttpPost]
    public async Task<IActionResult> AltaAutor(Autor autor)
    {
        await Ado.AltaAutorAsync(autor);
        // return RedirectToAction(nameof(ObtenerAutores));
        return View("../Author/AltaAutor");
    }
}