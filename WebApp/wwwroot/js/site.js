// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const regexPassword = (password) => {
    return password.match(
        "^.{8,}$"
    );
}

const validateEmail = (email) => {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};

const validatePassword = () => {
    const result = $('.result-password');
    const password = $('.input-password').val();
    const repeatPassword = $('#repeat-password').val();
    result.text('');
    $('#btn-register').prop('disabled', false);

    if (regexPassword(password)) {
        result.text('');
        $('#btn-register').prop('disabled', false);

        if (password == repeatPassword) {
            result.text('');
            $('.result-repeat-password').text('');
            $('#btn-register').prop('disabled', false);
        } else {
            result.text('password not match.');
            result.css('color', 'red');
            $('#btn-register').prop('disabled', true);
}
    } else {
        result.text('password minimum 8 characters');
        result.css('color', 'red');
        $('#btn-register').prop('disabled', true);
    }
}

const validateRepeatPassword = () => {
    var result = $('.result-repeat-password');
    const password = $('.input-password').val();
    const repeatPassword = $('#repeat-password').val();
    result.text('');
    $('#btn-register').prop('disabled', false);

    if (regexPassword(repeatPassword)) {
        result.text('');
        $('#btn-register').prop('disabled', false);

        if (repeatPassword == password) {
            result.text('');
            $('.result-password').text('');
            $('#btn-register').prop('disabled', false);
        } else {
            result.text('password not match.');
            result.css('color', 'red');
            $('#btn-register').prop('disabled', true);
        }
    } else {
        result.text('password minimum 8 characters');
        result.css('color', 'red');
        $('#btn-register').prop('disabled', true);
    }
}

const validate = () => {
    const $result = $('#result-email');
    const email = $('.email-input').val();
    $result.text('');
    $('#btn-register').prop('disabled', false);

    if (validateEmail(email)) {
        $result.text('');
        $('#btn-register').prop('disabled', false);
    } else {
        $result.text('email is invalid.');
        $result.css('color', 'red');
        $('#btn-register').prop('disabled', true);
    }
    return false;
}

$('.email-input').on('input', validate);
$('.input-password').on('input', validatePassword);
$('#repeat-password').on('input', validateRepeatPassword);