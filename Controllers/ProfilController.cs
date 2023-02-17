using MartialTime.DBProvider;
using MartialTime.Models;
using MartialTime.Models.Form;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Security;
using System.Data.Entity;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        [HttpGet]
        public IActionResult StudentProfil()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
                {
                    // Get all scheduled and previos courses...
                    int id = (int)HttpContext.Session.GetInt32("Id");
                    ViewBag.NextActivites = QueryDesigner.NextActivites(_context, id);
                    ViewBag.PreviousActivites = QueryDesigner.PreviousActivites(_context, id);
                    Random random = new Random();
                    ViewBag.Rn = float.Parse(String.Format("{0:0.0}", random.NextDouble() * (10 - 0) + 1));

                    return View();
                }
                TempData["permessionDenied"] = 0;
                return RedirectToAction(actionName: "SignIn", controllerName: "SignIn");
            }
            catch (Exception ex)
            {
                throw new Exception("Appication Error !" + ex.Message);
            }

        }
        [HttpGet]
        public IActionResult LogOut()
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Appication Error !" + ex.Message);
            }
            
        }
        [HttpGet]
        public IActionResult Infos()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
                {
                    var student = _context.Etudiants.Where(e => e.IdEtudiant == HttpContext.Session.GetInt32("Id")).FirstOrDefault();
                    ViewBag.Message = student;

                    return View();
                }
                // Redirect to Login Page
                TempData["permessionDenied"] = 0;
                return RedirectToAction(actionName: "SignIn", controllerName: "SignIn");
            }catch (Exception ex)
            {
                throw new Exception("Appication Error !" + ex.Message);
            }
            
        }
        [HttpPost]
        public void UpdatePassword(UpdatePasswordForm formData)
        {
            try
            {
                /*if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
                {
                    // Get Data From Form...
                    if (formData.OldPassword.Equals(HttpContext.Session.GetString("pwd")))
                    {
                        if (formData.NewPassword.Equals(formData.NewPassword))
                        {

                        }
                        else { TempData["passwordNotConforme"] = 0; }
                    }
                    else { TempData["passwordOldNotConforme"] = 0; }
                    Console.WriteLine("Hello ===========> ");
                    return RedirectToAction(actionName: "Profil", controllerName: "Infos");
                }
                // Redirect to Login Page
                TempData["permessionDenied"] = 0;
                return RedirectToAction(actionName: "SignIn", controllerName: "SignIn");*/
                    
            }catch (Exception ex)
            {
                throw new Exception("Appication Error ! " + ex.Message);
            }
            
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}