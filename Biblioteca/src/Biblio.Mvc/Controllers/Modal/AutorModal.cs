using Biblio.Core;
namespace Biblio.Mvc.Controllers;

public class AutorModal
{
    public  string Nombre { get; set; }
    public string Apellido { get; set; }
    public List<Autor> autores { get; set; } = [];
    public string busqueda { get; set; } = string.Empty;
    public AutorModal() {}
}
