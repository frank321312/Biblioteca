using Biblio.Core;
using Microsoft.AspNetCore.Mvc;
namespace Biblio.Mvc.Controllers;

public class EditorialController: Controller
{
    protected readonly IAdo Ado;
    private static readonly string _cadena =
        // @"Server=localhost;Database=5to_Biblioteca;Uid=5to_agbd;pwd=Trigg3rs!;Allow User Variables=True";
        @"Server=localhost;Database=5to_Biblioteca;Uid=root;pwd=root;Allow User Variables=True";
    public EditorialController()
    {
        Ado = new Biblio.AdoDapper.AdoDapper(_cadena);
    }

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
