using Microsoft.AspNetCore.Mvc;
using SensoreAPPMVC.Data;
using Microsoft.AspNetCore.Http;
using SensoreAPPMVC.Models; // Ensure AppDBContext is in this namespace

namespace SensoreAPPMVC.Controllers
{
    public class ClinicianController : Controller
    {
        private readonly SensoreAPPMVC.Data.AppDBContext _context;
        public ClinicianController(SensoreAPPMVC.Data.AppDBContext context)
        {
            _context = context;
        }
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Dashboard()
        {
            //validating clinician access
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetString("UserRole");

            if (userId == null || userRole != "Clinician")
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch necessary data for the clinician dashboard
            await Task.CompletedTask;

            //send view
            return View();
        }
    }
}