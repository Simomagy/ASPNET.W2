function togglePassword(id) {
    const pwdField = $("#" + id);
    const icon = $("#togglePasswordIcon");
    const confIcon = $("#toggleConfPasswordIcon");
    if (pwdField.attr("type") === "password") {
        pwdField.attr("type", "text");
        icon.removeClass("ti-eye");
        icon.addClass("ti-eye-off");
        confIcon.removeClass("ti-eye");
        confIcon.addClass("ti-eye-off");
    } else {
        pwdField.attr("type", "password");
        icon.removeClass("ti-eye-off");
        icon.addClass("ti-eye");
        confIcon.removeClass("ti-eye-off");
        confIcon.addClass("ti-eye");
    }
}

function valida() {
    const email = $("#email");
    const pwd = $("#pwd");
    const confirmPwd = $("#confirmPwd");
    const emailError = $("#emailError");
    const pwdError = $("#pwdError");
    const confirmPwdError = $("#confirmPwdError");
    let isValid = true;

    if (email.val() === "") {
        pwdError.text("Email is required.");
        emailError.removeClass("hidden");
        isValid = false;
    } else {
        emailError.addClass("hidden");
    }

    if (pwd.val() === "") {
        pwdError.text("Password is required.");
        pwdError.removeClass("hidden");
        isValid = false;
    } else {
        pwdError.addClass("hidden");
    }

    if (confirmPwd.val() === "") {
        confirmPwdError.text("Please confirm your password.");
        confirmPwdError.removeClass("hidden");
        isValid = false;
    }

    if (pwd.val() !== confirmPwd.val()) {
        confirmPwdError.text("Passwords do not match.");
        confirmPwdError.removeClass("hidden");
        isValid = false;
    }

    let regex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])");
    if (!regex.test(pwd.val())) {
        pwdError.text("Password must contain at least one lowercase letter, one uppercase letter, one number, and one special character.");
        pwdError.removeClass("hidden");
        isValid = false;
    } else {
        pwdError.addClass("hidden");
    }

    regex = new RegExp("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")
    if (!regex.test(email.val())) {
        emailError.text("Invalid email address.");
        emailError.removeClass("hidden");
        isValid = false;
    } else {
        emailError.addClass("hidden");
    }

    return isValid;
}
