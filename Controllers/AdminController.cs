

namespace ClinicQueueFrontend.Controllers;
using ClinicQueueFrontend.Models;
using ClinicQueueFrontend.Services;
using global::ClinicQueueFrontend.Models;
using global::ClinicQueueFrontend.Services;
using Microsoft.AspNetCore.Mvc;


    public class AdminController : Controller
    {
        private readonly ApiService _api;

        public AdminController(ApiService api)
        {
            _api = api;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

    public async Task<IActionResult> Users()
    {
        var users = await _api.GetAsync<List<User>>("/admin/users");

        //return View("UserList", users);

        return View(users);
    }

    public IActionResult CreateUser()
        {
            return View();
        }

        
 
    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        try
        {
            await _api.PostAsync<object>("/admin/users", user);
            return RedirectToAction("Users");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View(user);
        }
    }
}
