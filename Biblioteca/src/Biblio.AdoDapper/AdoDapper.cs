using System.Data;
using Biblio.Core;
using Dapper;
using MySqlConnector;

namespace Biblio.AdoDapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;
    public AdoDapper(string cadena)
    =>  _conexion = new MySqlConnection(cadena);

    private static readonly string _queryAutores 
        ="SELECT * FROM Autor ORDER BY apellido ASC,nombre ASC";
    private static readonly string _queryEditorial
        ="SELECT * FROM Editorial ORDER BY nombre ASC";
    private static readonly string _queryLibro
        ="SELECT * FROM Libro ORDER BY ISBN ASC";
    private static readonly string _queryTitulo
        ="SELECT * FROM Titulo ORDER BY Publicacion ASC";

    private static readonly string _queryFueraDeCirculacion
        ="SELECT * FROM FueraCirculacion ORDER BY FechaSalida ASC";
    private static readonly string _queryCurso
        ="SELECT * FROM Curso ORDER BY IdCurso ASC";
    private static readonly string _queryAlumno
        ="SELECT * FROM Alumno ORDER BY Dni ASC";
    #region Autor
    public void AltaAutor(Autor autor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdAutor",direction: ParameterDirection.Output);
        parametros.Add("@unNombre",autor.Nombre);
        parametros.Add("@unApellido",autor.Apellido);

        _conexion.Execute("altaAutor", parametros, commandType: CommandType.StoredProcedure);

        autor.IdAutor = parametros.Get<ushort>("unIdAutor");
    }

    public List<Autor> ObtenerAutores()
        =>_conexion.Query<Autor>(_queryAutores).ToList();

    #endregion
    
    #region Editorial
    public void AltaEditorial(Editorial editorial )
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdEditorial",direction: ParameterDirection.Output);
        parametros.Add("@unNombre",editorial.Nombre);

    }
    public List<Editorial>ObtenerEditorial()
        =>_conexion.Query<Editorial>(_queryEditorial).ToList();
    #endregion
    
    #region Libro
    public void AltaLibro(Libro libro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unISBN",direction: ParameterDirection.Output);
        parametros.Add("@unIdTitulo", libro.Titulo.IdTitulo);
        parametros.Add("@unIdEditorial", libro.Editorial.IdEditorial);
    }
    public List<Libro>ObtenerLibro()
        =>_conexion.Query<Libro>(_queryLibro).ToList();
    #endregion 
    
    #region Titulo
    
    public void AltaTitulo(Titulo titulo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unTitulo",direction: ParameterDirection.Output);
        parametros.Add("@unPublicacion",titulo.Publicacion);
        parametros.Add("@unIdTitulo",titulo.IdTitulo);
    }
    public List<Titulo>ObtenrTitulo()
        =>_conexion.Query<Titulo>(_queryTitulo).ToList();

    #endregion
    
    #region FueraDeCirculacion
    public void AltaFueraDeCirculacion(FueraCirculacion fueraCirculacion,Libro libro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unNumeroCopia",direction: ParameterDirection.Output);
        parametros.Add("@unISBN",libro.ISBN);
        parametros.Add("@unFechaSalida",fueraCirculacion.FechaSalida);
    }

    public List<FueraCirculacion> ObtenerFueraDeCirculacion()
        =>_conexion.Query<FueraCirculacion>(_queryFueraDeCirculacion).ToList();
    #endregion  
    
    #region Curso
        public void AltaCurso(Curso curso)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unIdCurso",direction: ParameterDirection.Output);
            parametros.Add("@unYear",curso.Year);
            parametros.Add("@unDivision",curso.Division);
        }
        public List<Curso>ObtenerCurso()
            =>_conexion.Query<Curso>(_queryCurso).ToList();

    #endregion

    #region  Alumno
        public void AltaAlumno(Alumno alumno, string pass)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unDni",direction: ParameterDirection.Output);
            parametros.Add("@unNombre ",alumno.Nombre);
            parametros.Add("@unApellido",alumno.Apellido);
            parametros.Add("@unCurso  ",alumno.Curso);
            parametros.Add("@unCelular ",alumno.Celular);
            parametros.Add("@unEmail  ",alumno.Email);
            parametros.Add("@unaContrasena",pass);
        }
        public List<Alumno>ObtenerAlumnos()
            => _conexion.Query<Alumno>(_queryAlumno).ToList();
    #endregion


}
