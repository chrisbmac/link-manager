using System;
using Microsoft.AspNetCore.Mvc;
using WebAppProj3.Models;

namespace WebAppProj3.Controllers
{
    public class LoginController : Controller
    {   
        // the login controller for accessing admin index
        public IActionResult Login() {
        return View();
        }

        public IActionResult Submit(string myUsername, string myPassword) {
            LinksLogin linkLogin = new LinksLogin(Connection.CONNECTION_STRING, HttpContext);
            // update properties
            linkLogin.username = myUsername;
            linkLogin.password = myPassword;
            // attempt to login!
            if (linkLogin.unlock()) {
                // access granted!
                return RedirectToAction("AdminIndex", "Admin");
            } else {
                // access denied
                ViewData["feedback"] = "Incorrect Login. Please try again...";
            }

            return View("Login");
        }
        
    }
}