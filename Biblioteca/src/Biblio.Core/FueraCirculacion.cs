namespace Biblio.Core;

public class FueraCirculacion
{
    public required byte NumeroCopia { get; set; }
    public required Libro libro{ get; set; }
    public required DateOnly FechaSalida { get; set; }
    public FueraCirculacion(byte NumeroCopia,Libro libro,DateOnly FechaSalida)
    {
        this.NumeroCopia=NumeroCopia;
        this.libro=libro;
        this.FechaSalida=FechaSalida;
    }
}
