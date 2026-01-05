//<script>
//(async () => {
//    const res = await fetch("/api/users/me", {credentials: "include" });
//    if (!res.ok) return;

//    const user = await res.json();
//    document.getElementById("navRight").innerHTML = `
//    <div class="user-menu">
//        <span class="user-name">${user.nickname}</span>
//        <div class="dropdown">
//            <a href="/profile.html">Profile</a>
//            <a href="/upload.html">Upload Game</a>
//            <hr>
//                <a href="#" onclick="logout()">Logout</a>
//        </div>
//    </div>
//    `;
//})();

//    async function logout() {
//        await fetch("/api/users/logout", { method: "POST", credentials: "include" });
//    location.href = "/";
//}
//</script>
