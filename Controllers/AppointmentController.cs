using ClinicQueueFrontend.Models;
using ClinicQueueFrontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicQueueFrontend.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApiService _api;

        public AppointmentController(ApiService api)
        {
            _api = api;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> Queue()
        {
            var queue = await _api.GetAsync<List<QueueEntry>>("/queue?date=2026-03-17");
            return View(queue);
        }
    }
   
}
