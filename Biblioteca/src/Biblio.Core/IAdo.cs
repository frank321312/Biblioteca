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
    Task<IEnumerable<Autor>>BuscarAutorAsync(string busqueda);
    
    Task AltaSolicitudAsync(Solicitud solicitud);
    Task<List<Solicitud>> ObtenerSolicitudAsync();
    
    Task AltaPrestamoAsync(Prestamo prestamo);
    Task<List<Prestamo>> ObtenerPrestamoAsync();

    Task AltaEditorialAsync(Editorial editorial);
    Task<List<Editorial>> ObtenerEditorialAsync();
    Task<IEnumerable<Editorial>>BuscarEditorialAsync(string busqueda);

    Task AltaLibroAsync(Libro libro);
    Task<List<Libro>>ObtenerLibroAsync();
    Task<IEnumerable<Libro>>BuscarLibroAsync(string busqueda);

    Task AltaTituloAsync(Titulo titulo);
    Task<List<Titulo>>ObtenerTituloAsync();
    Task<IEnumerable<Titulo>>BuscarTituloAsync(string busqueda);

    Task AltaFueraDeCirculacionAsync(FueraCirculacion fueraCirculacion,Libro libro);
    Task<List<Libro>> ObtenerFueraDeCirculacionAsync();
    
    Task<IEnumerable<FueraCirculacion>>BuscarFueraCirculacionAsync(string busqueda);
    
    Task AltaCursoAsync(Curso curso); 
    Task<List<Curso>>ObtenerCursoAsync();
    Task<IEnumerable<Curso>>BuscarCursoAsync(string busqueda); 

    Task AltaAlumnoAsync(Alumno alumno, string pass);
    Task<List<Alumno>> ObtenerAlumnosAsync();    
    Task<IEnumerable<Alumno>>BuscarAlumnoAsync(string busqueda);
    Task<Libro?> ObtenerLibroPorISBNAsync(ulong isbn);
    Task ActualizarFechaPrestamo(uint isbn,uint numeroCopia);
    Task<List<Libro?>>ObtenerLibroFueraDeCirculacionAsync(string busqueda);
    Task<List<Libro?>>ObtenerLibroPorBusquedaAsync(string busqueda);
    Task<Curso?> ObtenerCursoPorId(int idCurso);
    #endregion
}
