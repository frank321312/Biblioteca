using Biblio.Core;

namespace Biblio.Mvc.Controllers.Modal;

public class SolicitudModal
{
    public  uint IdSolicitud { get; set; }
    public  ulong ISBN { get; set; }
    public uint Dni { get; set; }
    public string busqueda { get; set; } = string.Empty;
    public  DateTime FechaSolicitud { get; set; }
    public  List<Solicitud> Solicitud { get; set; }=new List<Solicitud>();
    public  List<Alumno> Alumnos { get; set; }=new List<Alumno>();
    public List<Libro>Libros {get; set;}=new List<Libro>();
    public SolicitudModal(){}
}
