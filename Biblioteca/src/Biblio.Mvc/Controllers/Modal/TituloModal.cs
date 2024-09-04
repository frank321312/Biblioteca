using Biblio.Core;
namespace Biblio.Mvc.Controllers.Modal;

public class TituloModal
{
    public string? titulo { get; set; }
    public ushort Publicacion { get; set; }
    public List<Autor> autores = new List<Autor>();
    public List<Titulo> titulos = new List<Titulo>();
    // public List<int> idAutores { get; set; } = new List<int>();
    // public int AutorSeleccionado { get; set; }
    public TituloModal() {}
}
