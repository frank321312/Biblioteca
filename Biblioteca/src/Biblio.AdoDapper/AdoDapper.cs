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
    private static readonly string _queryEditorial
        ="SELECT * FROM Editorial ORDER BY nombre ASC";
    public void AltaEditorial(Editorial editorial )
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdEditorial",direction: ParameterDirection.Output);
        parametros.Add("@unNombre",editorial.Nombre);

    }
    public List<Editorial>ObtenerEditorial()
        =>_conexion.Query<Editorial>(_queryEditorial).ToList();
    #endregion
    
}
