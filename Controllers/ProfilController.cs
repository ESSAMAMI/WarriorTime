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
        [HttpGet]
        public IActionResult StudentProfil()
        {
            if (! string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return View();
            }
            TempData["permessionDenied"] = 0;
            return RedirectToAction(actionName: "SignIn", controllerName: "SignIn");

        }

        public IActionResult LogOut()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                // Clear Session...
                HttpContext.Session.Clear();
            }
            // Redirect to Login Page
            TempData["permessionDenied"] = 0;
            return RedirectToAction(actionName: "SignIn", controllerName: "SignIn");
        }
        
        public IActionResult Infos()
        {
            if(!string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                var student = _context.Etudiants.Where(e => e.IdEtudiant == HttpContext.Session.GetInt32("Id")).FirstOrDefault();
                ViewBag.Message = student;

                return View();
            }
            // Redirect to Login Page
            TempData["permessionDenied"] = 0;
            return RedirectToAction(actionName: "SignIn", controllerName: "SignIn");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}