using Biblio.Core;
using Biblio.Mvc.Controllers.Modal;
using Microsoft.AspNetCore.Mvc;

namespace Biblio.Mvc.Controllers
{
    public class LibroController:Controller
    {
        protected readonly IAdo Ado;
        private static readonly string _cadena =
            // @"Server=localhost;Database=5to_Biblioteca;Uid=5to_agbd;pwd=Trigg3rs!;Allow User Variables=True";
            @"Server=localhost;Database=5to_Biblioteca;Uid=root;pwd=root;Allow User Variables=True";
        public LibroController()
        {
            Ado = new Biblio.AdoDapper.AdoDapper(_cadena);
        }
        [HttpGet]
        public async Task<IActionResult> GetObtenerLibro()
        {
            var libro = await Ado.ObtenerLibroAsync();
            var ordenarlibro = libro.OrderBy(x => x.ISBN).ToList();
            return View("../Book/Libro",ordenarlibro);
        }
        
    }
}
