namespace Biblio.Core;

public interface IAdo
{

    void AltaAutor(Autor autor);
    List<Autor>ObtenerAutores();
    
    void AltaEditorial(Editorial editorial);
    List<Editorial>ObtenerEditorial();
    
    void AltaLibro(Libro libro);
    List<Libro>ObtenerLibro();
    
    void AltaTitulo(Titulo titulo);
    List<Titulo>ObtenerTitulo();
    
    void AltaFueraDeCirculacion(FueraCirculacion fueraCirculacion,Libro libro);
    List<FueraCirculacion> ObtenerFueraDeCirculacion();
    
    void AltaCurso(Curso curso); 
    List<Curso>ObtenerCurso();
    
    void AltaAlumno(Alumno alumno, string pass);
    List<Alumno> ObtenerAlumnos();    

    Libro? ObtenerLibroPorISBN(ulong isbn);
}
