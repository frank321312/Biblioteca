using Biblio.Core;
namespace Biblio.Mvc.Controllers.Modal;

public class TituloModal
{
    public string? nombre { get; set; }
    public ushort Publicacion { get; set; }
    public List<Autor> autores = new List<Autor>();
    public List<Titulo> titulos = new List<Titulo>();
    public List<int> autoresSeleccionados { get; set; } = new List<int>();
    public string autoresSeleccionadosString { get; set; } = string.Empty;
    public TituloModal() {}
}
