﻿using System.Data;
using Biblio.Core;
using Dapper;
using MySqlConnector;

namespace Biblio.AdoDapper;
public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;
    public AdoDapper(string cadena)
    => _conexion = new MySqlConnection(cadena);

    private static readonly string _queryAutores
        = "SELECT * FROM Autor ORDER BY apellido ASC,nombre ASC";
    private static readonly string _queryEditorial
        = "SELECT * FROM Editorial ORDER BY nombre ASC";
    private static readonly string _queryLibro
        = "SELECT * FROM Libro ORDER BY ISBN ASC";
    private static readonly string _queryTitulo
        = "SELECT * FROM Titulo ORDER BY Publicacion ASC";

    private static readonly string _queryFueraDeCirculacion
        = "SELECT * FROM FueraCirculacion ORDER BY FechaSalida ASC";
    private static readonly string _queryCurso
        = "SELECT * FROM Curso ORDER BY IdCurso ASC";
    private static readonly string _queryAlumno
        = "SELECT nombre ,apellido ,celular ,email ,DNI ,idCurso FROM Alumno ORDER BY Dni ASC";

    #region Autor
    public void AltaAutor(Autor autor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdAutor", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", autor.Nombre);
        parametros.Add("@unApellido", autor.Apellido);

        _conexion.Execute("altaAutor", parametros, commandType: CommandType.StoredProcedure);

        autor.IdAutor = parametros.Get<ushort>("unIdAutor");
    }

    public List<Autor> ObtenerAutores()
        => _conexion.Query<Autor>(_queryAutores).ToList();

    #endregion

    #region Editorial
    public void AltaEditorial(Editorial editorial)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdEditorial", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", editorial.Nombre);

        _conexion.Execute("altaEditorial", parametros, commandType: CommandType.StoredProcedure);
    }
    public List<Editorial> ObtenerEditorial()
        => _conexion.Query<Editorial>(_queryEditorial).ToList();
    #endregion

    #region Libro
    public void AltaLibro(Libro libro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unISBN", libro.ISBN);
        parametros.Add("@unIdTitulo", libro.Titulo.IdTitulo);
        parametros.Add("@unIdEditorial", libro.Editorial.IdEditorial);
        
        _conexion.Execute("altaLibro", parametros, commandType: CommandType.StoredProcedure);
    }
    public List<Libro> ObtenerLibro()
        => _conexion.Query<Libro>(_queryLibro).ToList();
    /*
    public void ObtenerLibroPorISBN(ulong isbn)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIsbn", isbn);
        
        _conexion.Query<Libro>("obtenerLibroISBN", parametros, commandType: CommandType.StoredProcedure);
    }*/


    private static readonly string _queryLibroISBN
        = @"SELECT  ISBN,
                    Libro.idTitulo, publicacion, titulo,
                    Libro.idEditorial, nombre
            FROM    Libro
            INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
            INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial
            WHERE   ISBN = @unIsbn
            LIMIT 1";
    public Libro? ObtenerLibroPorISBN(ulong isbn)
    {
        var libro = _conexion.Query<Libro, Titulo, Editorial, Libro>
            (_queryLibroISBN, 
                (libro, titulo, editorial) => 
                    {
                        libro.Titulo = titulo;
                        libro.Editorial = editorial;

                        return libro;
                    },
                new {unIsbn = isbn},
                splitOn: "idTitulo, idEditorial").
                FirstOrDefault();

        return libro;
    }


    private static readonly string _queryLibroTituloEditorial
        = @"SELECT  *
            FROM    Libro
            INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
            INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial
            WHERE   ISBN = @unIsbn";
    public Libro? ObtenerLibroPorISBN2(ulong isbn)
    {
        var libro = _conexion.Query<Libro, Titulo, Editorial, Libro>
            (_queryLibroTituloEditorial, 
                (libro, titulo, editorial) => 
                    {
                        libro.Titulo = titulo;
                        libro.Editorial = editorial;

                        return libro;
                    },
                new {unIsbn = isbn},
                splitOn: "ISBN").
                FirstOrDefault();

        if (libro is null)
            return null;
        
        return libro;
    }
    #endregion

    #region Titulo
    private readonly string queryInsertAutorTitulo
    = "	INSERT INTO AutorTitulo(idTitulo, idAutor)VALUES (@unIdTitulo, @unIdAutor)";
    public void AltaTitulo(Titulo titulo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unPublicacion", titulo.Publicacion);
        parametros.Add("@unTitulo", titulo.titulo);
        parametros.Add("@unIdTitulo", direction: ParameterDirection.Output);

        _conexion.Open();
        using (var transaccion = _conexion.BeginTransaction())
        {
            try
            {
                _conexion.Execute("altaTitulo", parametros, commandType: CommandType.StoredProcedure, transaction: transaccion);
                titulo.IdTitulo = parametros.Get<uint>("@unIdTitulo");

                var paraTitulo = titulo.Autores.
                    Select(a => new { unIdTitulo = titulo.IdTitulo, unIdAutor = a.IdAutor }).
                    ToList();

                _conexion.Execute(queryInsertAutorTitulo, paraTitulo, transaction: transaccion);

                //Como todo se ejecuto ok, confirmo los cambios
                transaccion.Commit();
            }
            catch (MySqlException e)
            {
                //Si hubo algun problema, doy marcha atras con los posibles cambios
                transaccion.Rollback();
                throw new InvalidOperationException(e.Message, e);
            }
        }
    }
    public List<Titulo> ObtenerTitulo()
        => _conexion.Query<Titulo>(_queryTitulo).ToList();

    #endregion

    #region FueraDeCirculacion
    public void AltaFueraDeCirculacion(FueraCirculacion fueraCirculacion, Libro libro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unNumeroCopia", direction: ParameterDirection.Output);
        parametros.Add("@unISBN", libro.ISBN);
        parametros.Add("@unFechaSalida", fueraCirculacion.FechaSalida);
    }

    public List<FueraCirculacion> ObtenerFueraDeCirculacion()
        => _conexion.Query<FueraCirculacion>(_queryFueraDeCirculacion).ToList();
    #endregion

    #region Curso
    public void AltaCurso(Curso curso)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdCurso", direction: ParameterDirection.Output);
        parametros.Add("@unanio", curso.anio);
        parametros.Add("@unDivision", curso.Division);
        _conexion.Execute("altaCurso", parametros, commandType: CommandType.StoredProcedure);
        curso.IdCurso = parametros.Get<byte>("unIdCurso");
    }
    public List<Curso> ObtenerCurso()
        => _conexion.Query<Curso>(_queryCurso).ToList();

    #endregion

    #region  Alumno
    public void AltaAlumno(Alumno alumno, string pass)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unDni", alumno.Dni);
        parametros.Add("@unNombre ", alumno.Nombre);
        parametros.Add("@unApellido", alumno.Apellido);
        parametros.Add("@unCelular ", alumno.Celular);
        parametros.Add("@unEmail", alumno.Email);
        parametros.Add("@unContraseña", pass);
        parametros.Add("@unIdCurso", alumno.IdCurso);
        _conexion.Execute("altaAlumno", parametros, commandType: CommandType.StoredProcedure);
    }
    public List<Alumno> ObtenerAlumnos()
        => _conexion.Query<Alumno>(_queryAlumno).ToList();
    #endregion


}