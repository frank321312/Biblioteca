using System.Data;
using Biblio.Core;
using Dapper;
using MySqlConnector;

namespace Biblio.AdoDapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;
    private readonly string _queryAutores 
        ="SELECT * FROM Autor ORDER BY apellido ASC,nombre ASC";
    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;
    public AdoDapper(string cadena)
      =>  _conexion = new MySqlConnection(cadena);
    public void AltaAutor(Autor autor)
    {
        throw new NotImplementedException();
    }

    public List<Autor> ObtenerAutores()
        =>_conexion.Query<Autor>(_queryAutores).ToList();
}
