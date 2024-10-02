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
        var orderEditoriales = editoriales.OrderBy(x => x.IdEditorial);
        return View("../Editorial/Editorial", editoriales);
    }

    [HttpGet]
    public IActionResult GetAltaEditorial(){
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
}
