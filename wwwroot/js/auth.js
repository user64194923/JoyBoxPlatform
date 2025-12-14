async function login() {
    const res = await fetch("/api/users/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify({
            email: email.value,
            password: password.value
        })
    });

    if (!res.ok) {
        error.textContent = "Invalid email or password";
        return;
    }

    location.href = "/";
}

async function register() {
    const res = await fetch("/api/users/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify({
            email: email.value,
            password: password.value,
            nickname: nickname.value
        })
    });

    if (!res.ok) {
        error.textContent = "Registration failed";
        return;
    }

    alert("Check your email to confirm your account");
    location.href = "/login.html";
}
