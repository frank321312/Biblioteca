namespace Biblio.Core;

public class Solicitud
{
    public required uint IdSolicitud { get; set; }
    public required Alumno alumno { get; set; }
    public required ulong ISBN { get; set; }
    public required DateTime FechaSolicitud { get; set; }
}
