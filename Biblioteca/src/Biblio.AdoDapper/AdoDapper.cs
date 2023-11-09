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
    #region Autor

    private static readonly string _queryAutores 
        ="SELECT * FROM Autor ORDER BY apellido ASC,nombre ASC";
    private static readonly string _queryEditorial
        ="SELECT * From Editorial ORDER BY nombre";
    private static readonly string _queryLibro
        ="SELECT * From Libro ORDER BY ISBN";
    private static readonly string _queryTitulo
        ="SELECT * From Titulo ORDER BY Publicacion";
    public void AltaAutor(Autor autor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdAutor",direction: ParameterDirection.Output);
        parametros.Add("@unNombre",autor.Nombre);
        parametros.Add("@unApellido",autor.Apellido);
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
}
