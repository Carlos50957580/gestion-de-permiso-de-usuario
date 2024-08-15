document.addEventListener('DOMContentLoaded', () => {
    // Selecciona los elementos
    const nombre = document.getElementById("Nombre");
    const apellido = document.getElementById("Apellido");
    const correo = document.getElementById("Correo");
    const fechaNacimiento = document.getElementById("FechaNacimiento");
    const telefono = document.getElementById("Telefono");
    const genero = document.getElementById("Genero");
    const form = document.getElementById("miFormulario");

    const validacionNombre = document.getElementById("validacionNombre");
    const validacionApellido = document.getElementById("validacionApellido");
    const validacionCorreo = document.getElementById("validacionCorreo");
    const validacionFechaNacimiento = document.getElementById("validacionFechaNacimiento");
    const validacionTelefono = document.getElementById("validacionTelefono");
    const validacionGenero = document.getElementById("validacionGenero");

    form.addEventListener("submit", e => {
        e.preventDefault(); // Evita el envío del formulario para validación

        // Inicializa los mensajes
        let entrar = false;
        let regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        // Limpia mensajes anteriores
        validacionNombre.innerHTML = "";
        validacionApellido.innerHTML = "";
        validacionCorreo.innerHTML = "";
        validacionFechaNacimiento.innerHTML = "";
        validacionTelefono.innerHTML = "";
        validacionGenero.innerHTML = "";

        // Validar nombre
        if (nombre.value.trim().length < 3) {
            validacionNombre.innerHTML = `El nombre no es válido`;
            entrar = true;
        }

        // Validar apellido
        if (apellido.value.trim().length < 4) {
            validacionApellido.innerHTML = `El apellido no es válido`;
            entrar = true;
        }

        // Validar correo
        if (!regexEmail.test(correo.value.trim())) {
            validacionCorreo.innerHTML = `El correo no es válido`;
            entrar = true;
        }

        // Validar fecha de nacimiento (opcional)
        if (!fechaNacimiento.value) {
            validacionFechaNacimiento.innerHTML = `Introduzca su fecha de nacimiento`;
            entrar = true;
        }

        // Validar teléfono (opcional)
        if (!telefono.value) {
            validacionTelefono.innerHTML = `Introduzca el número de teléfono`;
            entrar = true;
        }

        // Validar género
        if (genero.value === "") {
            validacionGenero.innerHTML = `Seleccione un género`;
            entrar = true;
        }

        // Mostrar mensajes de validación
        if (entrar) {
            return; // No enviar el formulario si hay errores
        } else {
             form.submit();
        }
    });
});
