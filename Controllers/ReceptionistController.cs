using ClinicQueueFrontend.Models;
using ClinicQueueFrontend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicQueueFrontend.Controllers
{
    public class ReceptionistController : Controller
    {
        private readonly ApiService _api;

        public ReceptionistController(ApiService api)
        {
            _api = api;
        }

        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("role");

            if (role != "receptionist")
                return RedirectToAction("Login", "Auth");

            return View();
        }

        public async Task<IActionResult> Queue()
        {
            try
            {
                var queue = await _api.GetAsync<List<QueueEntry>>("/queue?date=" + DateTime.Now.ToString("yyyy-MM-dd"));
                return View(queue);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<QueueEntry>());
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            try
            {
                var data = new
                {
                    status = status
                };

                await _api.PostAsync<object>($"/queue/{id}", data);

                return RedirectToAction("Queue");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Queue");
            }
        }
    }
}