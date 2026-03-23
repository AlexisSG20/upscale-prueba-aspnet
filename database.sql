CREATE DATABASE SistemaAccesoWebDB;
GO

USE SistemaAccesoWebDB;
GO

CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TipoDocumento NVARCHAR(10) NOT NULL,
    NumeroDocumento NVARCHAR(20) NOT NULL UNIQUE,
    Contrasena NVARCHAR(100) NOT NULL,

    Nombres NVARCHAR(100) NOT NULL,
    ApellidoPaterno NVARCHAR(100) NOT NULL,
    ApellidoMaterno NVARCHAR(100) NOT NULL,

    CorreoPrincipal NVARCHAR(150) NULL,
    CorreoSecundario NVARCHAR(150) NULL,
    TelefonoMovil NVARCHAR(20) NULL,
    TelefonoSecundario NVARCHAR(20) NULL,

    FechaNacimiento DATE NULL,
    Nacionalidad NVARCHAR(50) NULL,
    Sexo NVARCHAR(20) NULL,
    TipoContratacion NVARCHAR(50) NULL,
    FechaContratacion DATE NULL,
    Entidad NVARCHAR(150) NULL,
    Rol NVARCHAR(100) NULL,

    IntentosFallidos INT NOT NULL DEFAULT 0,
    EstaBloqueado BIT NOT NULL DEFAULT 0,
    BloqueadoHasta DATETIME NULL,
    Estado NVARCHAR(20) NOT NULL DEFAULT 'Activo'
);
GO

INSERT INTO Usuarios (
    TipoDocumento,
    NumeroDocumento,
    Contrasena,
    Nombres,
    ApellidoPaterno,
    ApellidoMaterno,
    CorreoPrincipal,
    CorreoSecundario,
    TelefonoMovil,
    TelefonoSecundario,
    FechaNacimiento,
    Nacionalidad,
    Sexo,
    TipoContratacion,
    FechaContratacion,
    Entidad,
    Rol
)
VALUES (
    'DNI',
    '46844596',
    'Admin123',
    'July Camila',
    'Mendoza',
    'Quispe',
    'test@minsa.gob.pe',
    NULL,
    '+51 999 999 999',
    NULL,
    '1944-04-15',
    'Peruana',
    'Femenino',
    'CAS',
    '2015-03-09',
    'Ministerio de Salud',
    'Administrador de Recursos'
);
GO

SELECT * FROM Usuarios;
GO


UPDATE Usuarios
SET IntentosFallidos = 0,
    EstaBloqueado = 0,
    BloqueadoHasta = NULL
WHERE NumeroDocumento = '46844596';