using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Mvc;
namespace Biblio.Mvc.Controllers;

public class TituloController : Controller
{
    protected readonly IAdo Ado;
    public TituloController(IAdo ado) => Ado = ado;

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
// Comentario
    [HttpGet]
    public async Task<IActionResult> ObtenerTitulosAutores()
    {
        var titulos = await Ado.ObtenerTituloAsync();
        var orderTitulos = titulos.OrderBy(x => x.IdTitulo).ToList();
        // var autores = await Ado.ObtenerAutoresAsync();
        // var orderAutores = autores.OrderBy(x => x.IdAutor).ToList();
        var tituloModal = new TituloModal
        {
            titulos = orderTitulos
        };
        foreach (var item in orderTitulos)
        {
            System.Console.WriteLine(item.Autores);
        }
        // foreach (var item in tituloModal.titulos)
        // {
        //     foreach (var icon in item.Autores)
        //     {
        //         System.Console.WriteLine(icon.Nombre);
        //     }
        // }

        return View("../Title/AutorTitulo", tituloModal);
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
