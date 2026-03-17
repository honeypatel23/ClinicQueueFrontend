using ClinicQueueFrontend.Models;
using ClinicQueueFrontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicQueueFrontend.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApiService _api;

        public PatientController(ApiService api)
        {
            _api = api;
        }

    
        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("role");

            if (role != "patient")
                return RedirectToAction("Login", "Auth");

            return View();
        }

       
        public async Task<IActionResult> MyAppointments()
        {
            try
            {
                var data = await _api.GetAsync<List<Appointment>>("/appointments/my");
                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<Appointment>());
            }
        }

        
        [HttpGet]
        public IActionResult BookAppointment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(string date, string timeSlot)
        {
            var role = HttpContext.Session.GetString("role");
            var userId = HttpContext.Session.GetInt32("userId");

            if (role != "patient" || userId == null)
                return RedirectToAction("Login", "Auth");

            try
            {
                
                var data = new
                {
                    date = date,
                    timeSlot = timeSlot,
                    patientId = userId,
                    status = "Pending"
                };

                await _api.PostAsync<object>("/appointments", data);

   
                return RedirectToAction("MyAppointments", "Patient");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}