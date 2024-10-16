using Biblio.Core;

namespace Biblio.Mvc.Controllers.Modal;

public class EditorialModal
{
    public string Nombre { get; set; } = string.Empty;
    public bool Error { get; set; } = false;
    public List<Editorial>editorials{get; set;}=new List<Editorial>();
    public EditorialModal() {}
}