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
        var autorModal = new AutorModal
        {
            autores=orderAutores
        };
        return View("../Author/Autor", autorModal);
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
        return RedirectToAction(nameof(ObtenerAutores));
    }
    
    [HttpGet]
    public async Task<IActionResult> BuscarAutor(string? busqueda)
    {
        var _autor = await Ado.ObtenerAutoresAsync();
        var autorModal = new AutorModal();
        if (busqueda == null)
            {
                autorModal.autores=_autor;
                return View("../Author/BusquedaAutor", autorModal);
            }
        IEnumerable<Autor>? autor = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            autor = await Ado.BuscarAutorAsync(busqueda);
            autorModal.autores=autor.ToList();
            if (autor.Count() == 0)
                return View("../Author/Autor", autorModal);
        }
        autor = autor ?? new List<Autor>();
        autorModal.autores=autor.ToList();
        return View("../Author/Autor", autorModal);
    }
}