function togglePasswordVisibility() {
    var passwordInput = document.getElementById("password");
    var passwordToggleBtn = document.getElementById("pass-toggle-btn");

    if (passwordInput.type === "password") {
        passwordInput.type = "text";
        passwordToggleBtn.classList.remove("fa-eye-slash");
        passwordToggleBtn.classList.add("fa-eye");
    } else {
        passwordInput.type = "password";
        passwordToggleBtn.classList.remove("fa-eye");
        passwordToggleBtn.classList.add("fa-eye-slash");
    }
}