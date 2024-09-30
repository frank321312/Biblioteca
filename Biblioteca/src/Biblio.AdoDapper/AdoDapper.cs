﻿using System.Data;
using System.Runtime.CompilerServices;
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
    /// <summary>
    /// Esta consulta tengo que trar el isbn nombre del titulo y nombre de la editorial
    /// </summary>
    private static readonly string _queryLibro
        = @"SELECT *
            FROM  Libro
            INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
            INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial";

    private static readonly string _queryTitulo
        = "SELECT * FROM Titulo ORDER BY Publicacion ASC";

    private static readonly string _queryTituloAutor
        = @"SELECT t.idTitulo, a.idAutor
            FROM Titulo t
            INNER JOIN AutorTitulo at ON at.idTitulo = t.idTitulo
            INNER JOIN Autor a ON a.idAutor = at.idAutor
            ORDER BY t.Publicacion ASC";

    private static readonly string _queryFueraDeCirculacion
        = "SELECT * FROM FueraDeCirculacion ORDER BY fechaEgreso ASC";
    private static readonly string _queryCurso
        = "SELECT * FROM Curso ORDER BY IdCurso ASC";
    private static readonly string _queryAlumno
        = "SELECT nombre ,apellido ,celular ,email ,DNI ,idCurso FROM Alumno ORDER BY Dni ASC";


    #region AutorAsync
    public async Task AltaAutorAsync(Autor autor)
    {
        DynamicParameters parametros = ParametrosParaAltaAutor(autor);

        await _conexion.ExecuteAsync("altaAutor", parametros, commandType: CommandType.StoredProcedure);

        autor.IdAutor = parametros.Get<ushort>("unIdAutor");
    }

    private static DynamicParameters ParametrosParaAltaAutor(Autor autor)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdAutor", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", autor.Nombre);
        parametros.Add("@unApellido", autor.Apellido);
        return parametros;
    }

    public async Task<List<Autor>> ObtenerAutoresAsync()
        => (await _conexion.QueryAsync<Autor>(_queryAutores)).ToList();

    #endregion

    #region EditorialAsync
    public async Task AltaEditorialAsync(Editorial editorial)
    {
        DynamicParameters parametros = ParametrosParaAltaEditorial(editorial);

        await _conexion.ExecuteAsync("altaEditorial", parametros, commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters ParametrosParaAltaEditorial(Editorial editorial)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdEditorial", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", editorial.Nombre);
        return parametros;
    }

    public async Task<List<Editorial>> ObtenerEditorialAsync()
        => (await _conexion.QueryAsync<Editorial>(_queryEditorial)).ToList();
    #endregion

    #region LibroAsync
    public async Task AltaLibroAsync(Libro libro)
    {
        DynamicParameters parametros = ParametrosParaAltaLibro(libro);

        await _conexion.ExecuteAsync("altaLibro", parametros, commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters ParametrosParaAltaLibro(Libro libro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unISBN", libro.ISBN);
        parametros.Add("@unIdTitulo", libro.Titulo.IdTitulo);
        parametros.Add("@unIdEditorial", libro.Editorial.IdEditorial);
        return parametros;
    }

    public async Task<List<Libro>> ObtenerLibroAsync()
    {
        var libros = (await _conexion.QueryAsync<Libro, Titulo, Editorial, Libro>(_queryLibro, (libro, titulo, editorial) => {
            libro.Titulo = titulo;
            libro.Editorial = editorial;
            return libro;
        },
        splitOn: "idTitulo, idEditorial"
        )).ToList();

        return libros;
    }

    //https://github.com/ET12DE1Computacion/SuperMercado/blob/Dapper/src/cSharp/Super.Dapper/AdoDapper.cs#L142
    private static readonly string _queryLibroDetalle
        =@"SELECT  *
            FROM    Libro
            WHERE   ISBN= @unIsbn

            SELECT  IdSolicitud, fechaSolitud
            FROM    Solicitud
            JOIN    Alumno USING (idSolicitud)
            WHERE   ISBN = @unIsbn;

            SELECT  
            FROM    Alumno
            JOIN Curso USING (idCurso)
            WHERE   DNI = @unDni;
            
            SELECT  
            FROM    Curso
            WHERE   idCurso = @unIdCurso;
            
";
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
    public async Task<Libro?> ObtenerLibroPorISBNAsync(ulong isbn)
    {
        var libro = (await _conexion.QueryAsync<Libro, Titulo, Editorial, Libro>
            (_queryLibroISBN,
                (libro, titulo, editorial) =>
                    {
                        libro.Titulo = titulo;
                        libro.Editorial = editorial;

                        return libro;
                    },
                new { unIsbn = isbn },
                splitOn: "idTitulo, idEditorial")).
                FirstOrDefault();
        if (libro is not null)
            libro.Titulo.Autores = (await ObtenerAutorPorISBNAsync(isbn)).Titulo.Autores;
        return libro;
    }

    private static readonly string _queryAutorISBN
        = @"SELECT 	ISBN,
                    Libro.idTitulo, publicacion, titulo,
                    Libro.idEditorial, Editorial.nombre,
                    Autor.idAutor, Autor.nombre, Autor.apellido
	        FROM	Libro
            JOIN Editorial USING (idEditorial)
            JOIN Titulo USING (idTitulo)
            JOIN AutorTitulo USING (idTitulo)
            JOIN Autor USING (idAutor)
            WHERE 	ISBN = @unIsbn
            ORDER BY Autor.apellido ASC, Autor.nombre ASC";
    public async Task<Libro?> ObtenerAutorPorISBNAsync(ulong isbn)
    {
        var libro = (await _conexion.QueryAsync<Libro, Titulo, Editorial, Autor, Libro>
            (_queryAutorISBN,
                (libro, titulo, editorial, autor) =>
                    {
                        libro.Titulo = titulo;
                        libro.Editorial = editorial;

                        if (titulo.Autores == null)
                        {
                            titulo.Autores = new List<Autor>();
                        }

                        titulo.Autores.Add(autor);

                        return libro;
                    },
                    new { unIsbn = isbn },
                    splitOn: "idTitulo, idEditorial, idAutor")).
                    FirstOrDefault();

        return libro;
    }
    #endregion

    #region TituloAsync
    private readonly string queryInsertAutorTitulo
    = "	INSERT INTO AutorTitulo(idTitulo, idAutor)VALUES (@unIdTitulo, @unIdAutor)";
    public async Task AltaTituloAsync(Titulo titulo)
    {
        var parametros = ParametrosParaAltaTitulo(titulo);

        _conexion.Open();
        using (var transaccion = _conexion.BeginTransaction())
        {
            try
            {
                await _conexion.ExecuteAsync("altaTitulo", parametros, commandType: CommandType.StoredProcedure, transaction: transaccion);
                titulo.IdTitulo = parametros.Get<uint>("@unIdTitulo");

                var paraTitulo = titulo.Autores.
                    Select(a => new { unIdTitulo = titulo.IdTitulo, unIdAutor = a.IdAutor }).
                    ToList();

                await _conexion.ExecuteAsync(queryInsertAutorTitulo, paraTitulo, transaction: transaccion);

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
    public async Task<List<Titulo>> ObtenerTituloAsync()
        => (await _conexion.QueryAsync<Titulo>(_queryTitulo)).ToList();

    public async Task<List<Titulo>> ObtenerTituloAutorAsync()
    {
        var titulosList = new List<Titulo>();
        var titulos = (await _conexion.QueryAsync<Titulo, Autor, Titulo>(_queryTituloAutor, (titulo, autor) => {
            var tituloExiste = titulosList.FirstOrDefault(x => x.IdTitulo == titulo.IdTitulo);

            if (tituloExiste == null)
            {
                titulo.Autores = new List<Autor>();
                titulo.Autores.Add(autor);
                titulosList.Add(titulo);
            }
            else
            {
                tituloExiste.Autores.Add(autor);
            }
            return titulo;
        }, splitOn: "idAutor"
        )).ToList();

        return titulosList;
    }

    #endregion

    #region FueraDeCirculacionAsync
    public async Task AltaFueraDeCirculacionAsync(FueraCirculacion fueraCirculacion, Libro libro)
    {
        DynamicParameters parametros = ParametrosParaAltaFueraDeCirculacion(fueraCirculacion, libro);
        await _conexion.ExecuteAsync("altaFueraDeCirculacion", parametros, commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters ParametrosParaAltaFueraDeCirculacion(FueraCirculacion fueraCirculacion, Libro libro)
    {
        var parametros = new DynamicParameters();
        // parametros.Add("@unNumeroCopia", direction: ParameterDirection.Output);
        parametros.Add("@unNumeroCopia", fueraCirculacion.NumeroCopia);
        parametros.Add("@unISBN", libro.ISBN);
        // parametros.Add("@unFechaSalida", fueraCirculacion.FechaSalida);
        return parametros;
    }

    public async Task<List<FueraCirculacion>> ObtenerFueraDeCirculacionAsync()
        => (await _conexion.QueryAsync<FueraCirculacion>(_queryFueraDeCirculacion)).ToList();
    #endregion

    #region CursoAsync
    public async Task AltaCursoAsync(Curso curso)
    {
        var parametros = ParametrosAltaCurso(curso);
        await _conexion.ExecuteAsync("altaCurso", parametros, commandType: CommandType.StoredProcedure);
        curso.IdCurso = parametros.Get<byte>("unIdCurso");
    }
    public async Task<List<Curso>> ObtenerCursoAsync()
        => (await _conexion.QueryAsync<Curso>(_queryCurso)).ToList();

    #endregion

    #region  AlumnoAsync
    public async Task AltaAlumnoAsync(Alumno alumno, string pass)
    {
        DynamicParameters parametros = ParametrosParaAltaAlumno(alumno, pass);
        await _conexion.ExecuteAsync("altaAlumno", parametros, commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters ParametrosParaAltaAlumno(Alumno alumno, string pass)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unDni", alumno.Dni);
        parametros.Add("@unNombre ", alumno.Nombre);
        parametros.Add("@unApellido", alumno.Apellido);
        parametros.Add("@unCelular ", alumno.Celular);
        parametros.Add("@unEmail", alumno.Email);
        parametros.Add("@unContrasena", pass);
        parametros.Add("@unIdCurso", alumno.IdCurso);
        return parametros;
    }

    public async Task<List<Alumno>> ObtenerAlumnosAsync()
        => (await _conexion.QueryAsync<Alumno>(_queryAlumno)).ToList();
    #endregion

    #region Autor
    public void AltaAutor(Autor autor)
    {
        var parametros = ParametrosParaAltaAutor(autor);

        _conexion.Execute("altaAutor", parametros, commandType: CommandType.StoredProcedure);

        autor.IdAutor = parametros.Get<ushort>("unIdAutor");
    }

    public List<Autor> ObtenerAutores()
        => _conexion.Query<Autor>(_queryAutores).ToList();

    #endregion

    #region Editorial
    public void AltaEditorial(Editorial editorial)
    {
        var parametros = ParametrosParaAltaEditorial(editorial);

        _conexion.Execute("altaEditorial", parametros, commandType: CommandType.StoredProcedure);
    }
    public List<Editorial> ObtenerEditorial()
        => _conexion.Query<Editorial>(_queryEditorial).ToList();
    #endregion

    #region Libro
    public void AltaLibro(Libro libro)
    {
        var parametros = ParametrosParaAltaLibro(libro);

        _conexion.Execute("altaLibro", parametros, commandType: CommandType.StoredProcedure);
    }
    public List<Libro> ObtenerLibro()
        => _conexion.Query<Libro>(_queryLibro).ToList();

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
                new { unIsbn = isbn },
                splitOn: "idTitulo, idEditorial").
                FirstOrDefault();
        if (libro is not null)
            libro.Titulo.Autores = ObtenerAutorPorISBN(isbn).Titulo.Autores;

        return libro;
    }
    public Libro? ObtenerAutorPorISBN(ulong isbn)
    {
        var libro = _conexion.Query<Libro, Titulo, Editorial, Autor, Libro>
            (_queryAutorISBN,
                (libro, titulo, editorial, autor) =>
                    {
                        libro.Titulo = titulo;
                        libro.Editorial = editorial;

                        if (titulo.Autores == null)
                        {
                            titulo.Autores = new List<Autor>();
                        }

                        titulo.Autores.Add(autor);

                        return libro;
                    },
                    new { unIsbn = isbn },
                    splitOn: "idTitulo, idEditorial, idAutor").
                    FirstOrDefault();

        return libro;
    }
    #endregion

    #region Titulo

    public void AltaTitulo(Titulo titulo)
    {
        DynamicParameters parametros = ParametrosParaAltaTitulo(titulo);

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

    private static DynamicParameters ParametrosParaAltaTitulo(Titulo titulo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unPublicacion", titulo.Publicacion);
        parametros.Add("@unNombre", titulo.nombre);
        parametros.Add("@unIdTitulo", direction: ParameterDirection.Output);
        return parametros;
    }

    public List<Titulo> ObtenerTitulo()
        => _conexion.Query<Titulo>(_queryTitulo).ToList();

    #endregion

    #region FueraDeCirculacion
    public void AltaFueraDeCirculacion(FueraCirculacion fueraCirculacion, Libro libro)
    {
        var parametros = ParametrosParaAltaFueraDeCirculacion(fueraCirculacion, libro);
        _conexion.Execute("altaFueraDeCirculacion", parametros, commandType: CommandType.StoredProcedure);
    }

    public List<FueraCirculacion> ObtenerFueraDeCirculacion()
        => _conexion.Query<FueraCirculacion>(_queryFueraDeCirculacion).ToList();
    #endregion

    #region Curso
    public void AltaCurso(Curso curso)
    {
        DynamicParameters parametros = ParametrosAltaCurso(curso);
        curso.IdCurso = parametros.Get<byte>("unIdCurso");
    }

    private DynamicParameters ParametrosAltaCurso(Curso curso)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unIdCurso", direction: ParameterDirection.Output);
        parametros.Add("@unanio", curso.anio);
        parametros.Add("@unDivision", curso.Division);
        // _conexion.Execute("altaCurso", parametros, commandType: CommandType.StoredProcedure);
        return parametros;
    }

    public List<Curso> ObtenerCurso()
        => _conexion.Query<Curso>(_queryCurso).ToList();

    #endregion

    #region  Alumno
    public void AltaAlumno(Alumno alumno, string pass)
    {
        var parametros = ParametrosParaAltaAlumno(alumno, pass);
        _conexion.Execute("altaAlumno", parametros, commandType: CommandType.StoredProcedure);
    }
    public List<Alumno> ObtenerAlumnos()
        => _conexion.Query<Alumno>(_queryAlumno).ToList();
    #endregion
}