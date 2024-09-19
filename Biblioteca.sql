DROP DATABASE IF EXISTS 5to_Biblioteca;
SELECT 'Creando BD' Estado;
CREATE DATABASE 5to_Biblioteca;
USE 5to_Biblioteca;

CREATE TABLE Titulo 
(
publicacion SMALLINT UNSIGNED NOT NULL,
nombre VARCHAR(45) NOT NULL,
idTitulo MEDIUMINT UNSIGNED  auto_increment,
CONSTRAINT PK_Titulo PRIMARY KEY (idTitulo),
CONSTRAINT UQ_Titulo_titulo UNIQUE (nombre)
);
CREATE TABLE Curso
(
anio TINYINT UNSIGNED NOT NULL,
division TINYINT UNSIGNED NOT NULL,
idCurso TINYINT UNSIGNED auto_increment,
CONSTRAINT PK_Curso PRIMARY KEY(idCurso),
CONSTRAINT UQ_Division_anio_division UNIQUE (anio ,division)
);
CREATE TABLE Autor 
(
nombre VARCHAR(45) NOT NULL,
apellido VARCHAR(45) NOT NULL,
idAutor SMALLINT UNSIGNED auto_increment,
CONSTRAINT PK_Autor PRIMARY KEY (idAutor)
);
CREATE TABLE Editorial
(
nombre VARCHAR(45) NOT NULL,
idEditorial SMALLINT UNSIGNED auto_increment,
CONSTRAINT PK_Editorial PRIMARY KEY (idEditorial),
CONSTRAINT UQ_Editorial_nombre UNIQUE (nombre)
);
CREATE TABLE Alumno 
(
nombre VARCHAR(45) NOT NULL,
apellido VARCHAR(45) NOT NULL,

celular INT UNSIGNED NOT NULL,
email VARCHAR(45) NOT NULL,
contrasena CHAR(64) NOT NULL,
DNI INT UNSIGNED NOT NULL,
idCurso TINYINT UNSIGNED NOT null,
CONSTRAINT PK_Alumno PRIMARY KEY (DNI),
CONSTRAINT FK_Curso_Alumno FOREIGN KEY (idCurso)
	REFERENCES Curso (idCurso)
);
CREATE TABLE AutorTitulo 
(
idTitulo MEDIUMINT UNSIGNED NOT NULL,
idAutor SMALLINT UNSIGNED NOT NULL,
CONSTRAINT PK_AutorTitulo PRIMARY KEY (idTitulo, idAutor),
CONSTRAINT FK_Titulo_AutorTitulo FOREIGN KEY (idTitulo)
	REFERENCES Titulo (idTitulo),
CONSTRAINT FK_Autor_AutorTitulo FOREIGN KEY (idAutor)
	REFERENCES Autor (idAutor)
);
CREATE TABLE Libro 
(
idTitulo MEDIUMINT UNSIGNED NOT NULL,
idEditorial SMALLINT UNSIGNED NOT NULL, 
ISBN BIGINT UNSIGNED NOT NULL,
cantidadPrestamo INT UNSIGNED NOT NULL,
CONSTRAINT PK_Libro PRIMARY KEY (ISBN),
CONSTRAINT FK_Editorial_Libro FOREIGN KEY (idEditorial)
	REFERENCES Editorial (idEditorial),
CONSTRAINT FK_Titulo_Libro FOREIGN KEY(idTitulo)
	REFERENCES Titulo (idTitulo)
);
CREATE TABLE Solicitud 
(
fechaSolicitud DATETIME NOT NULL,
idSolicitud INT UNSIGNED auto_increment,
DNI INT UNSIGNED NOT NULL,
ISBN BIGINT UNSIGNED NOT NULL,
CONSTRAINT PK_Solicitud PRIMARY KEY(idSolicitud),
CONSTRAINT FK_Alumno_Solicitud FOREIGN KEY(DNI)
	REFERENCES Alumno (DNI),
CONSTRAINT FK_Libro_Solicitud FOREIGN KEY(ISBN)
	REFERENCES Libro (ISBN)
);
CREATE TABLE FueraDeCirculacion 
(
fechaEgreso DATE NOT NULL, 
numeroCopia TINYINT UNSIGNED NOT NULL, 
ISBN BIGINT UNSIGNED NOT NULL, 
CONSTRAINT PK_FueraDeCirculacion PRIMARY KEY(numeroCopia, ISBN),
CONSTRAINT FK_Libro_FueraDeCirculacion FOREIGN KEY(ISBN)
	REFERENCES Libro (ISBN)
);
CREATE TABLE Prestamo 
(
fechaEgreso DATETIME NOT NULL, 
fechaRegreso DATETIME NOT NULL,
ISBN BIGINT UNSIGNED NOT NULL,
DNI INT UNSIGNED NOT NULL, 
numeroCopia TINYINT UNSIGNED NOT NULL,
CONSTRAINT PK_Prestamo PRIMARY KEY(fechaEgreso, ISBN, DNI, numeroCopia),
CONSTRAINT FK_Libro_Prestamo FOREIGN KEY(ISBN)
	REFERENCES Libro (ISBN),
CONSTRAINT FK_Alumno_Prestamo FOREIGN KEY(DNI)
	REFERENCES Alumno (DNI)
);
SELECT 'Creando SPs' Estado;
DELIMITER $$
CREATE PROCEDURE altaTitulo(IN unPublicacion VARCHAR(45),
							IN unNombre VARCHAR(45),
                            OUT unIdTitulo MEDIUMINT UNSIGNED)
BEGIN
	INSERT INTO Titulo(publicacion, nombre)
		VALUES (unPublicacion, unNombre);
	SET unIdTitulo = LAST_INSERT_ID();
END 
$$

DELIMITER $$
CREATE PROCEDURE altaCurso(IN unanio TINYINT UNSIGNED,
						   IN unDivision TINYINT UNSIGNED,
						   OUT unIdCurso TINYINT UNSIGNED)
BEGIN
	INSERT INTO Curso(anio, division, idCurso)
		VALUES (unanio, unDivision, unIdCurso);
	SET unIdCurso = LAST_INSERT_ID();
END
$$

DELIMITER $$
CREATE PROCEDURE altaAutor(IN unNombre VARCHAR(45),
						   IN unApellido VARCHAR(45),
						   OUT unIdAutor SMALLINT UNSIGNED)
BEGIN
	INSERT INTO Autor(nombre, apellido, idAutor)
		VALUES (unNombre, unApellido, unIdAutor);
	SET unIdAutor = LAST_INSERT_ID();

END
$$

DELIMITER $$
CREATE PROCEDURE altaEditorial(IN unNombre VARCHAR(45),
							   OUT unIdEditorial SMALLINT UNSIGNED)
BEGIN
	INSERT INTO Editorial(nombre, idEditorial)
		VALUES (unNombre, unIdEditorial);
	SET unIdEditorial = LAST_INSERT_ID();

END
$$

DELIMITER $$
CREATE PROCEDURE altaAlumno(IN unNombre VARCHAR(45),
							IN unApellido VARCHAR(45),
							IN unCelular INT UNSIGNED,
							IN unEmail VARCHAR(45),
							IN unContrasena CHAR(64),
							in unDNI INT UNSIGNED,
							IN unIdCurso TINYINT UNSIGNED)
BEGIN
	INSERT INTO Alumno(nombre, apellido,  celular, email, contrasena, DNI, idCurso)
		VALUES (unNombre, unApellido,  unCelular, unEmail, unContrasena, unDNI, unIdCurso);
END
$$

DELIMITER $$
CREATE PROCEDURE altaAutorTitulo(IN unIdTitulo MEDIUMINT,
								 IN unIdAutor SMALLINT)
BEGIN
	INSERT INTO AutorTitulo(idTitulo, idAutor)
		VALUES (unIdTitulo, unIdAutor);

END
$$

DELIMITER $$
CREATE PROCEDURE altaLibro(IN unIdTitulo MEDIUMINT UNSIGNED,
						   IN unIdEditorial SMALLINT UNSIGNED, 
						   IN unISBN BIGINT UNSIGNED)
BEGIN
	INSERT INTO Libro(idTitulo, idEditorial, ISBN, cantidadPrestamo)
		VALUES (unIdTitulo, unIdEditorial, unISBN, 0);
END
$$

DELIMITER $$
CREATE PROCEDURE altaSolicitud(IN unFechaSolicitud DATETIME,
							   IN unIdSolicitud INT UNSIGNED,
							   IN unDNI INT UNSIGNED,
							   IN unISBN BIGINT UNSIGNED)
BEGIN
	INSERT INTO Solicitud(fechaSolicitud, idSolicitud, DNI, ISBN)
		VALUES (unFechaSolicitud, unIdSolicitud, unDNI, unISBN);
END
$$

DELIMITER $$
CREATE PROCEDURE altaFueraDeCirculacion(IN unNumeroCopia TINYINT UNSIGNED, 
										IN unISBN  BIGINT UNSIGNED)
BEGIN
	INSERT INTO FueraDeCirculacion(fechaEgreso, numeroCopia, ISBN)
		VALUES (NOW(), unNumeroCopia, unISBN);
END
$$

DELIMITER $$
CREATE PROCEDURE altaPrestamo(IN unfechaRegreso DATETIME,
                              IN unfechaEgreso DATETIME,
                              IN unISBN INT UNSIGNED,
                              IN unDNI INT UNSIGNED,
                              IN unNumeroCopia TINYINT UNSIGNED)
BEGIN
    INSERT INTO Prestamo(fechaRegreso, fechaEgreso, ISBN, DNI, numeroCopia)
        VALUES  (unfechaRegreso, unfechaEgreso, unISBN, unDNI, unNumeroCopia);
END
$$

DELIMITER $$
CREATE FUNCTION PorcentajeFuera(unISBN BIGINT UNSIGNED)
								RETURNS DECIMAL(5, 2)
                                READS SQL DATA
BEGIN
	DECLARE porcentaje DECIMAL(5, 2);
    SELECT 	(COUNT(numeroCopia) / 200) * 100 INTO porcentaje
    FROM 	FueraDeCirculacion
    WHERE 	ISBN = unISBN;

    RETURN 	porcentaje;
END
$$
SELECT 'Creando Triggers' Estado $$
DELIMITER $$
CREATE TRIGGER fueraPrestamo BEFORE INSERT ON Prestamo
FOR EACH ROW
BEGIN
    IF( EXISTS (SELECT 1
				FROM	FueraDeCirculacion
                WHERE	numeroCopia = NEW.numeroCopia 
                AND		ISBN = NEW.ISBN
                )) THEN
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Ejemplar fuera de circulación';
	END IF;
END
$$

DELIMITER $$
CREATE TRIGGER aftPrestamo AFTER INSERT ON Prestamo
FOR EACH ROW
BEGIN
    UPDATE 	Libro
    SET 	cantidadPrestamo = cantidadPrestamo + 1
    WHERE 	ISBN = NEW.ISBN;
END
$$

DELIMITER $$
CREATE TRIGGER bfrPrestamo BEFORE INSERT ON Prestamo FOR EACH ROW
BEGIN
	IF (EXISTS	(SELECT *
		    	FROM Prestamo
	      		WHERE ISBN = NEW.ISBN
				AND numeroCopia = NEW.numeroCopia
				AND fechaRegreso IS NULL)
		) THEN
		SIGNAL SQLSTATE '45000'
		SET MESSAGE_TEXT = 'Error, el libro no esta disponible';
	END IF;
END
$$

SELECT 'Generando Inserts' Estado$$

CALL altaTitulo(2004, 'Head First Design Patterns', @idHeadFirst);
CALL altaEditorial('O REILLY', @idHeadEditorial);
CALL altaLibro(@idHeadEditorial, 1, 596007124);

CALL altaAutor('Eric', 'Freeman', @IdHeadAutor);
CALL altaAutor('Elisabeth', 'Robson', @IdHeadAutor1);
CALL altaAutor('Bert', 'Bates', @IdHeadAutor2);
CALL altaAutor('Kathy', 'Sierra', @IdHeadAutor3);
CALL altaAutorTitulo( @idHeadFirst,@IdHeadAutor1);
CALL altaAutorTitulo( @idHeadFirst,@IdHeadAutor2);
CALL altaAutorTitulo( @idHeadFirst,@IdHeadAutor3);
CALL altaAutorTitulo(@idHeadFirst,@IdHeadAutor);
CALL altaFueraDeCirculacion(1, 596007124);
CALL altaAutor("Tito","joel",@IdHeadAutor5);
CALL altaCurso(5, 7, @IdHeadCurso);
CALL altaAlumno('Pepito', 'Perez', 1125648696, 'pepito11@gmail.com', 'contrasena', 48186408, 1);
CALL altaPrestamo('2023-09-08', '2023-09-01', 596007124, 48186408, 3);
CALL altaPrestamo('2023-09-09', '2023-09-02', 596007124, 48186408, 4);
CALL altaPrestamo('2023-09-10', '2023-09-03', 596007124, 48186408, 5);

-- SELECT PorcentajeFuera(596007124) FROM FueraDeCirculacion LIMIT 1;
SELECT * FROM `FueraDeCirculacion`;
