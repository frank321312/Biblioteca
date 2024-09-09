using System.Diagnostics.CodeAnalysis;

namespace Biblio.Core;
public class Libro
{
    public required Titulo Titulo { get; set; }
    public required Editorial Editorial { get; set; }
    public required ulong ISBN { get; set; }

    List<Solicitud>Solicitudes{get; set;}
    List<FueraCirculacion> FueraCirculaciones{get; set;}
    List<Prestamo>Prestamos{get; set;}

    public Libro(ulong ISBN) => this.ISBN = ISBN;

    [SetsRequiredMembers]
    public Libro(Titulo titulo , Editorial Editorial,ulong ISBN)
    {
        this.Titulo=titulo;
        this.Editorial=Editorial;
        this.ISBN=ISBN;
        Prestamos=new List<Prestamo>();
        FueraCirculaciones=new List<FueraCirculacion>();
        Solicitudes=new List<Solicitud>();
    }

    public Libro() {}
}
