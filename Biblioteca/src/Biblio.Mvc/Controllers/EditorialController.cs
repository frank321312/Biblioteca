using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Mvc;
namespace Biblio.Mvc.Controllers;

public class EditorialController : Controller
{
    protected readonly IAdo Ado;
    public EditorialController(IAdo ado) => Ado = ado;

    [HttpGet]
    public async Task<IActionResult> ObtenerEditoriales()
    {
        var editoriales = await Ado.ObtenerEditorialAsync();
        var orderEditoriales = editoriales.OrderBy(x => x.IdEditorial).ToList();
        var editorialModal = new EditorialModal
        {
            editorials = orderEditoriales
        };
        return View("../Editorial/Editorial", editorialModal);
    }

    [HttpGet]
    public IActionResult GetAltaEditorial()
    {
        var editorialModal = new EditorialModal();
        return View("../Editorial/AltaEditorial", editorialModal);
    }

    [HttpPost]
    public async Task<IActionResult> PostEditorial(EditorialModal editorial)
    {
        try
        {
            await Ado.AltaEditorialAsync(new Editorial(editorial.Nombre));
            return RedirectToAction(nameof(ObtenerEditoriales));
        }
        catch (MySqlConnector.MySqlException)
        {
            var editorialModal = new EditorialModal();
            editorialModal.Error = true;
            return View("../Editorial/AltaEditorial", editorialModal);
        }
    }
    [HttpGet]
    public async Task<IActionResult> BuscarEditorial(string? busqueda)
    {
        var _editorial = await Ado.ObtenerEditorialAsync();
        var editorialModal = new EditorialModal();
        if (busqueda == null)
        {
            editorialModal.editorials = _editorial;
            return View("../Editorial/Editorial", editorialModal);
        }

        IEnumerable<Editorial>? editorial = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            editorial = await Ado.BuscarEditorialAsync(busqueda);
            editorialModal.editorials = editorial.ToList();
            if (editorial.Count() == 0)
                return View("../Editorial/Editorial", editorialModal);
        }
        editorial = editorial ?? new List<Editorial>();
        editorialModal.editorials = editorial.ToList();
        return View("../Editorial/Editorial", editorialModal);
    }

    [HttpGet]
    public async Task<IActionResult> DetallesDeLibros(uint id)
    {   
        var libros = await Ado.ObtenerLibroAsync();
        var libroeditorial = libros.Where(x => x.Editorial.IdEditorial == id).ToList();
        var ordelarLibro = libroeditorial.OrderBy(x => x.Titulo).ToList();
        var libroModal = new LibroModal
        {
            libros = ordelarLibro,
        };
        return View("../Book/Libro", libroModal);
    }
}

