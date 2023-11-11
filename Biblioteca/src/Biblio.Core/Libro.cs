namespace Biblio.Core;
public class Libro
{
    public required Titulo Titulo { get; set; }
    public required Editorial Editorial { get; set; }
    public required ulong Isbn { get; set; }
    List<Solicitud>Solicitudes{get; set;}
    List<FueraCirculacion> FueraCirculaciones{get; set;}
    List<Prestamo>Prestamos{get; set;}
    public Libro()
    {
        this.Prestamos=new List<Prestamo>();
        this.FueraCirculaciones=new List<FueraCirculacion>();
        this.Solicitudes=new List<Solicitud>();
    }
}
