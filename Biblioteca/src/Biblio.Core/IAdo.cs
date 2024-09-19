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
    Libro? ObtenerAutorPorISBN(ulong isbn);
    #region Methodasync
    Task AltaAutorAsync(Autor autor);
    Task<List<Autor>> ObtenerAutoresAsync();
    
    Task AltaEditorialAsync(Editorial editorial);
    Task<List<Editorial>> ObtenerEditorialAsync();

    Task AltaLibroAsync(Libro libro);
    Task<List<Libro>>ObtenerLibroAsync();

    Task AltaTituloAsync(Titulo titulo);
    Task<List<Titulo>>ObtenerTituloAsync();
    Task<List<Titulo>>ObtenerTituloAutorAsync();

    Task AltaFueraDeCirculacionAsync(FueraCirculacion fueraCirculacion,Libro libro);
    Task<List<FueraCirculacion>> ObtenerFueraDeCirculacionAsync();
    
    Task AltaCursoAsync(Curso curso); 
    Task<List<Curso>>ObtenerCursoAsync();
    
    Task AltaAlumnoAsync(Alumno alumno, string pass);
    Task<List<Alumno>> ObtenerAlumnosAsync();    

    Task<Libro?> ObtenerLibroPorISBNAsync(ulong isbn);
    Task<Libro?> ObtenerAutorPorISBNAsync(ulong isbn);
    #endregion
}
