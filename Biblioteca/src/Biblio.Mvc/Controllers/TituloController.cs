using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Mvc;
namespace Biblio.Mvc.Controllers;

public class TituloController : Controller
{
    protected readonly IAdo Ado;
    private static readonly string _cadena =
        // @"Server=localhost;Database=5to_Biblioteca;Uid=5to_agbd;pwd=Trigg3rs!;Allow User Variables=True";
        @"Server=localhost;Database=5to_Biblioteca;Uid=root;pwd=root;Allow User Variables=True";
    public TituloController()
    {
        Ado = new Biblio.AdoDapper.AdoDapper(_cadena);
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTitulos()
    {
        var titulos = await Ado.ObtenerTituloAsync();
        var orderTitulos = titulos.OrderBy(x => x.IdTitulo).ToList();
        return View("../Title/Titulo", orderTitulos);
    }

    [HttpGet]
    public async Task<IActionResult> GetAltaTitulo()
    {
        var autores = await Ado.ObtenerAutoresAsync();
        var orderAutores = autores.OrderBy(x => x.IdAutor).ToList();
        var tituloModal = new TituloModal();
        tituloModal.autores = orderAutores;
        return View("../Title/AltaTitulo", tituloModal);
    }

    [HttpPost]
    public async Task<IActionResult> AltaTitulo(TituloModal titleModal)
    {
        if (titleModal.titulo == null)
        {
            throw new InvalidOperationException("El nombre no puede ser null");
        }
        var title = new Titulo(titleModal.Publicacion, titleModal.titulo);
        title.Autores = titleModal.autores;
        await Ado.AltaTituloAsync(title);
        return RedirectToAction(nameof(GetAltaTitulo));
    }
}
