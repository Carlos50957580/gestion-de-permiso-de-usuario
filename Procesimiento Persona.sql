select * from Personas

create proc sp_RegistrarPersona(
@Nombre varchar(15),
@Apellido nvarchar(15),
@FechaNacimiento Date,
@Genero varchar (10),
@Telefono nvarchar(20),
@Correo nvarchar(100),
@Mensaje varchar(500) output,
@Resultado int output
) 
as
begin
	SET @Resultado = 0

	IF NOT EXISTS (SELECT * FROM Personas WHERE Correo = @Correo)
	begin 
		insert into Personas(Nombre, Apellido, FechaNacimiento, Genero, Telefono, Correo) values
		(@Nombre, @Apellido, @FechaNacimiento, @Genero, @Telefono, @Correo)

		SET @Resultado = SCOPE_IDENTITY()
	end
	else
	set @Mensaje = 'El correro de esta persona ya existe'
end

go

create proc sp_EditarUsuario(
@PersonaID  int,
@Nombre varchar(15),
@Apellido nvarchar(15),
@FechaNacimiento Date,
@Genero varchar (10),
@Telefono nvarchar(20),
@Correo nvarchar(100),
@Mensaje varchar(500) output,
@Resultado bit output
)
as 
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Personas WHERE Correo = @Correo and PersonaID != @PersonaID)
	begin

	update top (1) Personas set
	Nombre = @Nombre,
	Apellido = @Apellido,
	FechaNacimiento = @FechaNacimiento,
	Genero = @Genero,
	Telefono = @Telefono,
	Correo = @Correo
	Where PersonaID = @PersonaID

	SET @Resultado = 1
	end
	else 
	set @Mensaje = 'El correro de esta persona ya existe'
end

