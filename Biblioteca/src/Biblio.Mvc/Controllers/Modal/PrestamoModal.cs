using Biblio.Core;

namespace Biblio.Mvc.Controllers.Modal;

public class PrestamoModal
{
    public ulong ISBN { get; set; }
    public  byte NumeroCopia { get; set; }
    public  uint Dni { get; set; }
    public  DateTime FechaEgreso { get; set; }
    public  DateTime FechaRegreso { get; set; }
    public List<Prestamo> Prestamos{ get; set; }=new List<Prestamo>();
    public List<Libro> Libros { get; set; }=new List<Libro>();
    public PrestamoModal(){}
}
