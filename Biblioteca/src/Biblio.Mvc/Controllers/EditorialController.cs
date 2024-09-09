using Biblio.Core;
using Microsoft.AspNetCore.Mvc;
namespace Biblio.Mvc.Controllers;

public class EditorialController: Controller
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
    public IActionResult GetAltaEditorial()
        => View("../Editorial/AltaEditorial");

    [HttpPost]
    public async Task<IActionResult> PostEditorial(Editorial editorial)
    {
        await Ado.AltaEditorialAsync(editorial);
        return View("../Editorial/AltaEditorial");
    }
}
