using ClinicQueueFrontend.Models;
using ClinicQueueFrontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicQueueFrontend.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _api;

        public AuthController(ApiService api)
        {
            _api = api;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var data = new
                {
                    email = email,
                    password = password
                };

                var result = await _api.PostAsync<LoginResponse>("/auth/login", data);

               
                HttpContext.Session.SetString("token", result.token);
                HttpContext.Session.SetString("role", result.user.role);
                HttpContext.Session.SetInt32("userId", result.user.id);

                if (result.user.role == "doctor")
                    return RedirectToAction("Dashboard", "Doctor");

                if (result.user.role == "patient")
                    return RedirectToAction("Dashboard", "Patient");

                if (result.user.role == "receptionist")
                    return RedirectToAction("Dashboard", "Receptionist");

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
