// form-validation.js

$(document).ready(function () {
    // Función para validar el formato del correo electrónico
    function validateEmail(email) {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return regex.test(email);
    }

    $('#registerForm').on('submit', function (e) {
        e.preventDefault();

        const email = $('#yourEmail').val();

        if (!validateEmail(email)) {
            $('#message').html('<div class="alert alert-danger">El correo electrónico no es válido. Debe contener "@" y ".com".</div>');
            return;
        }

        $.ajax({
            url: '@Url.Action("Registrar", "Login")',
            type: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                $('#message').html('<div class="alert alert-success">Registro exitoso!</div>');
            },
            error: function (xhr, status, error) {
                $('#message').html('<div class="alert alert-danger">Hubo un problema al registrar. Intente de nuevo.</div>');
            }
        });
    });
});
