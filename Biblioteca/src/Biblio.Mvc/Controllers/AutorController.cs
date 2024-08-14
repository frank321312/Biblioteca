
using Biblio.Core;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers;
public class AutorController :Controller
{
    readonly IAdo _ado;

    public AutorController(IAdo ado) => _ado = ado;


    public async Task<ViewResult> Index()
    {
        var autores = await _ado.ObtenerAutoresAsync();
        return View(autores);
    }
}
