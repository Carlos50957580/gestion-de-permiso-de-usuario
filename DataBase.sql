CREATE TABLE Usuarios (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Correo NVARCHAR(100) NOT NULL UNIQUE,
    HashContraseña NVARCHAR(255) NOT NULL,
	Estado int default 1,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Roles (
    RolID INT IDENTITY(1,1) PRIMARY KEY,
    NombreRol NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255),
	Estado int default 1,
);

CREATE TABLE Permisos (
    PermisoID INT IDENTITY(1,1) PRIMARY KEY,
    NombrePermiso NVARCHAR(50) NOT NULL,
    Descripcion NVARCHAR(255),
	Estado int default 1,
);

CREATE TABLE UsuarioRoles (
    UsuarioID INT NOT NULL,
    RolID INT NOT NULL,
	Estado int default 1,
    PRIMARY KEY (UsuarioID, RolID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);

CREATE TABLE RolPermisos (
    RolID INT NOT NULL,
    PermisoID INT NOT NULL,
	Estado int default 1,
    PRIMARY KEY (RolID, PermisoID),
    FOREIGN KEY (RolID) REFERENCES Roles(RolID),
    FOREIGN KEY (PermisoID) REFERENCES Permisos(PermisoID)
);
