using Biblio.Core;

namespace Biblio.Mvc.Controllers.Modal;

public class FueraCirculacionModal
{
    public byte NumeroCopia { get; set; }
    public ulong ISBN { get; set; }
    public string fechaEgreso { get; set; }
    public List<Libro> libros { get; set; } = new List<Libro>();
    public FueraCirculacionModal(byte num, ulong isbn, string fecha) 
    {
        NumeroCopia = num;
        ISBN = isbn;
        fechaEgreso = fecha;
    }

    public FueraCirculacionModal() { }
}
