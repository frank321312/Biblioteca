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
        var titleModal = await CrearTituloModal();
        return View("../Title/AltaTitulo", titleModal);
    }

    public async Task<TituloModal> CrearTituloModal()
    {
        var autores = await Ado.ObtenerAutoresAsync();
        var orderAutores = autores.OrderBy(x => x.IdAutor).ToList();
        var tituloModal = new TituloModal();
        tituloModal.autores = orderAutores;
        return tituloModal;
    }

    [HttpPost]
    public async Task<IActionResult> AltaTitulo(TituloModal titleModal)
    {
        try
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
            return RedirectToAction(nameof(ObtenerTitulos));
        }
        catch (InvalidOperationException)
        {
            var tituloModal = await CrearTituloModal();
            tituloModal.error = true;
            return View("../Title/AltaTitulo", tituloModal);
        }
    }

    [HttpGet]
    public async Task<IActionResult> BuscarTitulo(string? busqueda)
    {
        var _titulo = await Ado.ObtenerTituloAsync();
        var tituloModal = new TituloModal();
        if (busqueda == null)
        {
            tituloModal.titulos = _titulo;
            return View("../Student/BusquedaAlumno", tituloModal);
        }

        IEnumerable<Titulo>? titulo = null;
        if (!string.IsNullOrEmpty(busqueda))
        {
            titulo = await Ado.BuscarTituloAsync(busqueda);
            tituloModal.titulos = titulo.ToList();
            if (titulo.Count() == 0)
                return View("../Student/BusquedaAlumno", tituloModal);
        }
        titulo = titulo ?? new List<Titulo>();
        tituloModal.titulos = titulo.ToList();
        return View("../Student/BusquedaAlumno", tituloModal);
    }
}