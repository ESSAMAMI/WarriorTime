using MartialTime.DBProvider;
using MartialTime.Models.Form;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MartialTime.Controllers
{
    public class SignInController : Controller
    {
        private readonly WarriortimeContext _context;
        private readonly ILogger<ProfilController> _logger;

        public SignInController(WarriortimeContext context, ILogger<ProfilController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Get: SignIn
        public IActionResult SignIn()
        {
            return View();
        }

        // POST: SignIn/OpenSession
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public RedirectToActionResult OpenSession(SignInForm signIn)
        {
            try
            {
                if (signIn.userType != null && signIn.userType.Equals("student"))
                {
                    var student = QueryDesigner.Login(_context, signIn);
                    if (student != null)
                    {
                        // Connection OK !
                        // Open Session...
                        HttpContext.Session.SetString("Token", Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
                        HttpContext.Session.SetInt32("Id", student.IdEtudiant);
                        HttpContext.Session.SetString("Name", student.Nom);
                        HttpContext.Session.SetString("SurName", student.Prenom);
                        HttpContext.Session.SetString("Email", student.Email);
                        HttpContext.Session.SetString("Tel", student.Telephone);
                        HttpContext.Session.SetString("Pwd", student.Mdp);
                        // Redirect to User Profil
                        return RedirectToAction(actionName: "StudentProfil", controllerName: "Profil");
                    }
                }
                TempData["connectionStat"] = 0;
                return RedirectToAction(actionName: "SignIn", controllerName: "SignIn");
            }
            catch (Exception ex)
            {
                throw new Exception("Appication Error !" + ex.Message);
            }

        }

    }
}