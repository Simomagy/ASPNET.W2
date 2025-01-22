function togglePassword(id) {
    const pwdField = document.getElementById(id);
    const icon = $("#togglePasswordIcon");
    if (pwdField.type === "password")
    {
        pwdField.type = "text";
        icon.removeClass("ti-eye");
        icon.addClass("ti-eye-off");
    }
    else
    {
        pwdField.type = "password";
        icon.removeClass("ti-eye-off");
        icon.addClass("ti-eye");
    }
}

function valida() {
    const email = document.getElementById("email");
    const pwd = document.getElementById("pwd");
    const emailError = $("#emailError");
    const pwdError = $("#pwdError");
    let isValid = true;

    if (email.value === "") {
        emailError.removeClass("hidden");
        isValid = false;
    }
    else {
        emailError.addClass("hidden");
    }

    if (pwd.value === "") {
        pwdError.removeClass("hidden");
        isValid = false;
    }
    else {
        pwdError.addClass("hidden");
    }

    return isValid;
}
