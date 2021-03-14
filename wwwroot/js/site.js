function startAutoLogout() {
    // redirect back to login page when session expires
    window.setTimeout(() => document.location = "/Login/Login", 1200000);
    // warn user when session about to expire
    window.setTimeout(() => document.getElementById("lblExpire").innerHTML = "WARNING : Session is about to expire!", 1080000);
}