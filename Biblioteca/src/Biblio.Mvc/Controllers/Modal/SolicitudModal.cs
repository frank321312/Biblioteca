using Biblio.Core;

namespace Biblio.Mvc.Controllers.Modal;

public class SolicitudModal
{


    public  uint IdSolicitud { get; set; }
    public  ulong ISBN { get; set; }
    public  DateTime FechaSolicitud { get; set; }
    public  List<Alumno> Alumnos { get; set; }=new List<Alumno>();
    public List<Libro>Libros {get; set;}=new List<Libro>();
    public SolicitudModal(){}
}
