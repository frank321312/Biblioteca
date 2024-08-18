using Biblio.Core;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;
public class AutorController : Controller
{    protected readonly IAdo Ado;
    private static readonly string _cadena =
        @"Server=localhost;Database=5to_Biblioteca;Uid=root;pwd=root;Allow User Variables=True";
    public AutorController() 
    {
        Ado = new Biblio.AdoDapper.AdoDapper(_cadena);
    }

    [HttpGet("/autor")]
    public async Task<ViewResult> ObtenerAutores()
    {
        var autores = await Ado.ObtenerAutoresAsync();
        AutorModal autor = new AutorModal();
        var orderAutores = autores.OrderBy(x => x.IdAutor).ToList();
        autor.SetAutores(orderAutores);
        return View("../Author/Autor", autor);
    }

    [HttpPost("/autor")]
    public async Task<ViewResult> AltaAutor(Autor autor)
    {
        await Ado.AltaAutorAsync(autor);
        return await ObtenerAutores();
    }
}