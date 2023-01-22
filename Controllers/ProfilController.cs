using MartialTime.DBProvider;
using MartialTime.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MartialTime.Controllers
{
    public class ProfilController : Controller
    {
        private readonly ILogger<ProfilController> _logger;
        private readonly WarriortimeContext _context;
        public ProfilController(ILogger<ProfilController> logger, WarriortimeContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Get: /Profil/StudentProfil
        public IActionResult StudentProfil()
        {
            return View();
        }

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}