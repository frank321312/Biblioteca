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
        var libroModal = new LibroModal
        {
            libros = ordenarlibro
        };
        return View("../Book/Libro", libroModal);
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
        return RedirectToAction(nameof(ObtenerLibro));
    }

    [HttpGet]
    public async Task<IActionResult> BuscarLibro(string? busqueda)
    {
        var _libro = await Ado.ObtenerLibroAsync();
        var libroModal = new LibroModal();
        if (busqueda == null)
        {
            libroModal.libros = _libro;
            return View("../Book/Libro", libroModal);
        }
        IEnumerable<Libro>? libro = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            libro = await Ado.ObtenerLibroPorBusquedaAsync(busqueda);
            libroModal.libros = libro.ToList();
            if (libro.Count() == 0)
                return View("../Book/Libro", libroModal);
        }
        libro = libro ?? new List<Libro>();
        libroModal.libros = libro.ToList();
        return View("../Book/Libro", libroModal);
    }

    [HttpGet]
    public async Task<IActionResult> DetallesDeLibros(ulong isbn)
    {
        var libros = await Ado.ObtenerLibroAsync();
        var libroeditorial = libros.Where(x => x.ISBN == isbn).ToList();
        var ordelarLibro = libroeditorial.OrderBy(x => x.Titulo).ToList();
        var libroModal = new LibroModal
        {
            libros = ordelarLibro,
        };
        return View("../Book/Libro", libroModal);
    }
}
