using Biblio.Core;

namespace Biblio.Mvc.Controllers.Modal;

public class FueraCirculacionModal
{
    public byte NumeroCopia { get; set; }
    public ulong ISBN { get; set; }
    public string fechaEgreso { get; set; }
    public List<Libro> libros { get; set; } = new List<Libro>();
    public List<Titulo> titulos{get; set;}= new List<Titulo>();
    public List<FueraCirculacion> fueraCirculacion{get; set;}= new List<FueraCirculacion>();
    public FueraCirculacionModal(byte num, ulong isbn, string fecha) 
    {
        NumeroCopia = num;
        ISBN = isbn;
        fechaEgreso = fecha;
    }

    public FueraCirculacionModal() { }
}
