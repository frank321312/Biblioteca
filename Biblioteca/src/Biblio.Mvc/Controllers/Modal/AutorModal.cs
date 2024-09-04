using Biblio.Core;
namespace Biblio.Mvc.Controllers;

public class AutorModal
{
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public AutorModal() {}
}
