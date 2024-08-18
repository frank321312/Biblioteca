using Biblio.Core;
namespace Biblio.Mvc.Controllers;

public class AutorModal
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public List<Autor>? autores;
    public AutorModal() {}
    public void SetAutores(List<Autor> autors)
        => autores = autors;
}
