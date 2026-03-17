using ClinicQueueFrontend.Models;
using ClinicQueueFrontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicQueueFrontend.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApiService _api;
        public DoctorController(ApiService api) 
        {
            _api = api;
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> Queue()
        {
            var queue = await _api.GetAsync<List<QueueEntry>>("/doctor/queue");

            return View(queue);
        }
    }
}
