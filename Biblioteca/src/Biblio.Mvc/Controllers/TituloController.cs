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

    [HttpGet]
    public async Task<IActionResult> ObtenerTitulosAutores()
    {
        var titulos = await Ado.ObtenerTituloAutorAsync();
        var orderTitulos = titulos.OrderBy(x => x.IdTitulo).ToList();

        return View("../Title/AutorTitulo", orderTitulos);
    }

    [HttpPost]
    public async Task<IActionResult> AltaTitulo(TituloModal titleModal)
    {
        if (titleModal.nombre == null)
        {
            throw new InvalidOperationException("El nombre no puede ser null");
        }

        Titulo title = new Titulo(titleModal.Publicacion, titleModal.nombre);
        var autores = await Ado.ObtenerAutoresAsync();
        List<int> seleccionados = titleModal.autoresSeleccionadosString
            .Split(',')
            .Select(s => Convert.ToInt32(s))
            .ToList();

        foreach (var a in seleccionados)
        {
            Autor autor = autores.First(x => x.IdAutor == a);
            title.Autores.Add(autor);
        }
        await Ado.AltaTituloAsync(title);
        return RedirectToAction(nameof(GetAltaTitulo));
    }
}