CREATE TABLE Personas (
  PersonaID int PRIMARY KEY IDENTITY(1,1),
  Nombre nvarchar(15) NOT NULL,
  Apellido nvarchar(15) NOT NULL,
  FechaNacimiento DATE,
  Genero varchar (10),
  Telefono int,
  Correo nvarchar(100) NOT NULL UNIQUE,
  CreadoPor nvarchar (15) NOT NULL,
  FechaCambio DATETIME DEFAULT GETDATE(),
  ActualizadoPor nvarchar (15) not null

);


CREATE TABLE Roles (
  RolID int PRIMARY KEY IDENTITY(1,1),
  NombreRol nvarchar(15) NOT NULL,
  Descripcion nvarchar(255),
  Estado int NOT NULL DEFAULT 1,
  CreadoPor nvarchar (15) NOT NULL,
  FechaCambio DATETIME DEFAULT GETDATE(),
  ActualizadoPor nvarchar (15) not null
);

CREATE TABLE Usuarios (
  UsuarioID int PRIMARY KEY IDENTITY(1,1),
  NombreUsuario nvarchar (15) NOT NULL, 
  PersonaID int NOT NULL UNIQUE,
  RolID int NOT NULL,
  Contraseña nvarchar(255) NOT NULL,
  FechaCambio DATETIME DEFAULT GETDATE(),
  Estado int NOT NULL DEFAULT 1,
  CreadoPor nvarchar (15) NOT NULL,
  ActualizadoPor  nvarchar (15) not null
  FOREIGN KEY (PersonaID) REFERENCES Personas(PersonaID),
  FOREIGN KEY (RolID) REFERENCES Roles(RolID)
);


CREATE TABLE Permisos (
  PermisoID int PRIMARY KEY IDENTITY(1,1),
  NombrePermiso nvarchar(50) NOT NULL,
  Descripcion nvarchar(255),
  Estado int NOT NULL DEFAULT 1,
  CreadoPor nvarchar (15) NOT NULL,
  FechaCambio DATETIME DEFAULT GETDATE(),
  ActualizadoPor nvarchar (15) not null
);

CREATE TABLE RolPermisos (
  RolPermisoID int PRIMARY KEY IDENTITY(1,1),
  RolID int NOT NULL,
  PermisoID int NOT NULL,
  FOREIGN KEY (RolID) REFERENCES Roles(RolID),
  FOREIGN KEY (PermisoID) REFERENCES Permisos(PermisoID),
  Estado int NOT NULL DEFAULT 1,
  CreadoPor nvarchar (15) NOT NULL,
  FechaCambio DATETIME DEFAULT GETDATE(),
  ActualizadoPor nvarchar (15) not null 
);