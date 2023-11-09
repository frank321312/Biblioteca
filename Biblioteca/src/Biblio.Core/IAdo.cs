namespace Biblio.Core;

public interface IAdo
{
    void AltaAutor(Autor autor);
    List<Autor>ObtenerAutores();
    void AltaEditorial(Editorial editorial);
    List<Editorial>ObtenerEditorial();
    void AltaLibro(Libro libro);
    List<Libro>ObtenerLibro();
    
    /*void AltaTitulo(Titulo titulo);
    List<Titulo>ObtenrTitulo();
    void AltaFueraDeCirculacion();*/


    

}
