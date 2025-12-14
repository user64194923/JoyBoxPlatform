document.addEventListener("DOMContentLoaded", () => {
    const loginEmail = document.getElementById("loginEmail");
    const loginPassword = document.getElementById("loginPassword");
    const authError = document.getElementById("authError");
    const loginBtn = document.getElementById("loginBtn");

    async function login() {
        authError.textContent = "";

        const res = await fetch("/api/users/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            credentials: "include",
            body: JSON.stringify({
                email: loginEmail.value,
                password: loginPassword.value
            })
        });

        if (!res.ok) {
            authError.textContent = "Invalid email/nickname or password.";
            return;
        }

        alert("Logged in successfully!");
        // Optionally redirect or load profile
    }

    loginBtn.addEventListener("click", login);
});


// Make register global
window.register = async function () {
    const email = document.getElementById('email');
    const nickname = document.getElementById('nickname');
    const regPassword = document.getElementById('regPassword');
    const errorDiv = document.getElementById('error');

    errorDiv.textContent = '';
    errorDiv.innerHTML = '';

    if (!email.value || !nickname.value || !regPassword.value) {
        errorDiv.textContent = "All fields are required.";
        return;
    }

    const res = await fetch("/api/users/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            email: email.value,
            nickname: nickname.value,
            password: regPassword.value
        })
    });

    if (!res.ok) {
        const data = await res.json();
        let messages = [];

        if (Array.isArray(data)) {
            if (typeof data[0] === "string") {
                // Array of strings
                messages = data;
            } else if (data[0].description) {
                // Array of objects with description
                messages = data.map(e => e.description).filter(d => d && d.trim() !== "");
            }
        } else if (data && data.errors) {
            for (const key in data.errors) {
                if (Array.isArray(data.errors[key])) {
                    messages.push(...data.errors[key]);
                }
            }
        }

        if (messages.length === 0) messages = ["Registration failed"];
        errorDiv.innerHTML = messages.map(m => `<div>${m}</div>`).join("");
        return;
    }


    alert("Registered! Check your email to confirm.");
};




async function login() {
    const res = await fetch("/api/users/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify({
            email: loginEmail.value,
            password: loginPassword.value
        })
    });

    const authError = document.getElementById('authError');
    authError.textContent = "";

    if (!res.ok) {
        const errText = await res.text();
        authError.textContent = "Invalid email/nickname or password.";
        return;
    }

    await loadProfile();
}

