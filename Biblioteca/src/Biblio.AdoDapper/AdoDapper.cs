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
    private static readonly string _querySolicitud = "SELECT * FROM Solicitud ORDER BY fechaSolicitud ASC";
    private static readonly string _queryPrestamo = "SELECT * FROM Prestamo ORDER BY fechaEgreso ASC";
    private static readonly string _queryAutores
        = "SELECT * FROM Autor ORDER BY apellido ASC,nombre ASC";
    private static readonly string _queryEditorial
        = "SELECT * FROM Editorial ORDER BY nombre ASC";
    private static readonly string _queryLibro
        = @"SELECT *
            FROM  Libro
            INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
            INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial";

    private static readonly string _queryTitulo
        = "SELECT * FROM Titulo ORDER BY Publicacion ASC";

    private static readonly string _queryFueraDeCirculacion
        = "SELECT * FROM FueraDeCirculacion ORDER BY fechaEgreso ASC";
    private static readonly string _queryCurso
        = "SELECT * FROM Curso ORDER BY IdCurso ASC";
    private static readonly string _queryAlumno
        = "SELECT nombre ,apellido ,celular ,email ,DNI ,idCurso FROM Alumno ORDER BY Dni ASC";
    private static readonly string _searchFueraCirculacion
        = @"SELECT  F.numeroCopia, F.ISBN, T.nombre
        FROM Libro L
        INNER JOIN Titulo T ON Libro.idTitulo = Titulo.idTitulo
        INNER JOIN FueraDeCirculacion F ON L.ISB = F.ISB
        WHERE T.nombre LIKE @unNombre
        OR ISBN LIKE @unISBN ";
    private readonly string _searchLibro
    = @" SELECT *
        FROM Libro
        INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
        INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial
        WHERE Titulo.nombre LIKE @unNombre
        OR  Editorial.nombre LIKE @unEditorial
    ";
    private static readonly string _searchTitulo
        = @"SELECT *
        FROM Titulo
        WHERE nombre LIKE @unNombre
        OR publicacion LIKE @unPublicacion";
    private readonly string _searchAlumno
        = @"SELECT *
        FROM Alumno
        WHERE nombre LIKE @unNombre
        OR apellido LIKE @unApellido  ";
    private readonly string _searchAutor
        = @"SELECT *
        FROM Autor
        WHERE nombre LIKE @unNombre
        OR apellido LIKE @unApellido ";
    private readonly string _searchCurso
    = @" SELECT *
        FROM Curso
        WHERE anio LIKE @anio
        OR division LIKE @unDivision
    ";
    private readonly string _searchEditorial
    = @" SELECT *
        FROM Editorial
        WHERE nombre LIKE @unNombre
        OR  idEditorial LIKE @unIdEditorial 
    ";

    private static readonly string _queryFueraLibro
    = @"SELECT 	Libro.ISBN,
                    Libro.idTitulo, Titulo.publicacion, Titulo.nombre,
                    Editorial.idEditorial, Editorial.nombre
	        FROM	Libro
            INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
            INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial
            INNER JOIN FueraDeCirculacion ON Libro.ISBN = FueraDeCirculacion.ISBN
            ORDER BY FueraDeCirculacion.fechaEgreso ASC";

    private static readonly string _actualizarPrestamo
    = @"UPDATE Prestamo SET fechaRegreso=@unFecha WHERE ISBN = @unIsbn AND numeroCopia=@unNumeroCopia ";

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
        var libros = (await _conexion.QueryAsync<Libro, Titulo, Editorial, Libro>(_queryLibro, (libro, titulo, editorial) =>
        {
            libro.Titulo = titulo;
            libro.Editorial = editorial;
            return libro;
        },
        splitOn: "idTitulo, idEditorial, nombre, nombre"
        )).ToList();

        return libros;
    }

    //https://github.com/ET12DE1Computacion/SuperMercado/blob/Dapper/src/cSharp/Super.Dapper/AdoDapper.cs#L142
    private static readonly string _queryLibroDetalle
        = @"SELECT  *
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
        = @"SELECT  Libro.ISBN,
                    Libro.idTitulo, Titulo.publicacion, Titulo.nombre,
                    Libro.idEditorial
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
        // if (libro is not null)
        //     libro.Titulo.Autores = (await ObtenerAutorPorISBNAsync(isbn)).Titulo.Autores;
        return libro;
    }


    private static readonly string _queryAutorISBN
        = @"SELECT 	ISBN,
                    Libro.idTitulo, Titulo.publicacion, Titulo.nombre,
                    Libro.idEditorial, Editorial.nombre
	        FROM	Libro
            INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
            INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial
            WHERE Titulo.nombre LIKE @unNombre
            OR  Editorial.nombre LIKE @unEditorial";
    public async Task<List<Libro?>> ObtenerLibroPorBusquedaAsync(string busqueda)
    {
        var libro = (await _conexion.QueryAsync<Libro, Titulo, Editorial, Libro>
            (_queryAutorISBN,
                (libro, titulo, editorial) =>
                    {
                        libro.Titulo = titulo;
                        libro.Editorial = editorial;
                        return libro;
                    },
                    new { unNombre = "%" + busqueda + "%", unEditorial = "%" + busqueda + "%" },
                    splitOn: "idTitulo, idEditorial")).ToList();
        return libro;
    }
    private static readonly string _queryFuera
    = @"SELECT 	Libro.ISBN,
                    Libro.idTitulo, Titulo.publicacion, Titulo.nombre,
                    Editorial.idEditorial, Editorial.nombre
	        FROM	Libro
            INNER JOIN Titulo ON Libro.idTitulo = Titulo.idTitulo
            INNER JOIN Editorial ON Libro.idEditorial = Editorial.idEditorial
            INNER JOIN FueraDeCirculacion ON Libro.ISBN = FueraDeCirculacion.ISBN
            WHERE Titulo.nombre LIKE @unNombre
            OR  Editorial.nombre LIKE @unEditorial";
    public async Task<List<Libro?>> ObtenerLibroFueraDeCirculacionAsync(string busqueda)
    {
        var libro = (await _conexion.QueryAsync<Libro, Titulo, Editorial, FueraCirculacion, Libro>
            (_queryFuera,
                (libro, titulo, editorial, fueraCirculacion) =>
                    {
                        libro.Titulo = titulo;
                        libro.Editorial = editorial;
                        libro.FueraCirculacion = fueraCirculacion;

                        return libro;
                    },
                    new { unNombre = "%" + busqueda + "%", unEditorial = "%" + busqueda + "%" },
                    splitOn: "idTitulo, idEditorial")).ToList();
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

    public async Task<List<Libro>> ObtenerFueraDeCirculacionAsync()
    {
        {
            var libro = (await _conexion.QueryAsync<Libro, Titulo, Editorial, FueraCirculacion, Libro>
                (_queryFueraLibro,
                    (libro, titulo, editorial, fueraCirculacion) =>
                        {
                            libro.Titulo = titulo;
                            libro.Editorial = editorial;
                            libro.FueraCirculacion = fueraCirculacion;

                            return libro;
                        },
                        splitOn: "idTitulo, idEditorial, isbn")).ToList();
            return libro;
        }
    }
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

    public async Task<IEnumerable<Alumno>> BuscarAlumnoAsync(string busqueda)
    {

        var parametros = new { unNombre = "%" + busqueda + "%", unApellido = "%" + busqueda + "%" };
        return await _conexion.QueryAsync<Alumno>(_searchAlumno, parametros);
    }
    #endregion










    #region Solicitud
    public async Task AltaSolicitudAsync(Solicitud solicitud)
    {
        DynamicParameters parametros = ParametrosParaAltaSolicitud(solicitud);
        await _conexion.ExecuteAsync("altaSolicitud", parametros, commandType: CommandType.StoredProcedure);
    }

    private static DynamicParameters ParametrosParaAltaSolicitud(Solicitud solicitud)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unFechaSolicitud", solicitud.FechaSolicitud);
        parametros.Add("@unISBN", solicitud.ISBN);
        parametros.Add("@unDNI", solicitud.Dni);
        parametros.Add("@unIdSolicitud", direction: ParameterDirection.Output);
        return parametros;
    }

    public async Task<List<Solicitud>> ObtenerSolicitudAsync()
     => (await _conexion.QueryAsync<Solicitud>(_querySolicitud)).ToList();
    #endregion





    #region Prestamo
    private static DynamicParameters ParametrosParaAltaPrestamo(Prestamo prestamo)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unfechaRegreso", prestamo.FechaRegreso);
        parametros.Add("@unfechaEgreso", prestamo.FechaEgreso);
        parametros.Add("@unNumeroCopia", prestamo.NumeroCopia);
        parametros.Add("@unDNI", prestamo.Dni);
        parametros.Add("@unISBN", prestamo.ISBN);
        return parametros;
    }
    public async Task AltaPrestamoAsync(Prestamo prestamo)
    {
        DynamicParameters parametros = ParametrosParaAltaPrestamo(prestamo);
        await _conexion.ExecuteAsync("altaPrestamo", parametros, commandType: CommandType.StoredProcedure);

    }

    public async Task<List<Prestamo>> ObtenerPrestamoAsync()
   => (await _conexion.QueryAsync<Prestamo>(_queryPrestamo)).ToList();

    #endregion







    #region Busquedas

    public async Task<IEnumerable<Autor>> BuscarAutorAsync(string busqueda)
    {
        var parametros = new { unNombre = "%" + busqueda + "%", unApellido = "%" + busqueda + "%" };
        return await _conexion.QueryAsync<Autor>(_searchAutor, parametros);
    }

    public async Task<IEnumerable<Curso>> BuscarCursoAsync(string busqueda)
    {
        var parametros = new { unanio = "%" + busqueda + "%", unDivision = "%" + busqueda + "%" };
        return await _conexion.QueryAsync<Curso>(_searchCurso, parametros);
    }

    public async Task<IEnumerable<Editorial>> BuscarEditorialAsync(string busqueda)
    {
        var parametros = new { unNombre = "%" + busqueda + "%", unIdEditorial = "%" + busqueda + "%" };
        return await _conexion.QueryAsync<Editorial>(_searchEditorial, parametros);
    }

    public async Task<IEnumerable<Libro>> BuscarLibroAsync(string busqueda)
    {
        var parametros = new { unNombre = "%" + busqueda + "%", unEditorial = "%" + busqueda + "%" };
        return await _conexion.QueryAsync<Libro>(_searchLibro, parametros);
    }

    public async Task<IEnumerable<Titulo>> BuscarTituloAsync(string busqueda)
    {
        var parametros = new { unNombre = "%" + busqueda + "%", unPublicacion = "%" + busqueda + "%" };
        return await _conexion.QueryAsync<Titulo>(_searchTitulo, parametros);
    }

    public async Task<IEnumerable<FueraCirculacion>> BuscarFueraCirculacionAsync(string busqueda)
    {
        var parametros = new { unNombre = "%" + busqueda + "%", unISBN = "%" + busqueda + "%" };
        return await _conexion.QueryAsync<FueraCirculacion>(_searchFueraCirculacion, parametros);
    }

    public async Task  ActualizarFechaPrestamo(uint isbn,uint numeroCopia)
    {
        DateTime FechaEgreso= DateTime.Now;
        var parametros = new { unIsbn = isbn, unFecha = FechaEgreso,unNumeroCopia=numeroCopia };
        await _conexion.QueryAsync<Prestamo>(_actualizarPrestamo, parametros);
    }
    #endregion
}