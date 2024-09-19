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
        return View("../Book/Libro", ordenarlibro);
    }

    [HttpGet]
    public async Task<IActionResult> GetAltaLibro()
    {
        var titulos = await Ado.ObtenerTituloAsync();
        var editoriales = await Ado.ObtenerEditorialAsync();
        var libro = new LibroModal();
        libro.editoriales = editoriales;
        libro.titulos = titulos;
        return View("../Book/AltaLibro", libro);
    }

    [HttpPost]
    public async Task<IActionResult> AltaLibro(LibroModal bookModal)
    {
        var titulos = await Ado.ObtenerTituloAsync();
        var editoriales = await Ado.ObtenerEditorialAsync();
        var titulo = titulos.First(x => x.IdTitulo == bookModal.idTitulo);
        var editorial = editoriales.First(x => x.IdEditorial == bookModal.idEditorial);
        Libro libro = new Libro(titulo, editorial, bookModal.ISBN);
        await Ado.AltaLibroAsync(libro);
        return RedirectToAction(nameof(GetAltaLibro));
    }
}
