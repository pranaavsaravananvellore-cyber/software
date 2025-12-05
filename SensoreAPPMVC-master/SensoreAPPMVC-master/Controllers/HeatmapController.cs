using Microsoft.AspNetCore.Mvc;
using SensoreAPPMVC.Services;
using SensoreAPPMVC.Data;
using Microsoft.AspNetCore.Http; // <-- Add this if missing
using System.Threading.Tasks;

namespace SensoreAPPMVC.Controllers
{
    public class HeatmapController : Controller
    {
        private readonly HeatmapStorageService _heatmapService;
        private readonly AppDBContext _context;

        public HeatmapController(HeatmapStorageService HeatmapService, AppDBContext context)
        {
            _heatmapService = HeatmapService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int patientId, IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length == 0)
            {
                ViewBag.Error = "Please select a CSV file.";
                return View();
            }

            using var stream = csvFile.OpenReadStream();

            await _heatmapService.StoreHeatmapsFromCsvAsync(patientId, stream);

            ViewBag.Message = "Heatmap CSV processed successfully.";

            return View();
        }
    }
}
