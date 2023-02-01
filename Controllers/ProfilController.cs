using MartialTime.DBProvider;
using MartialTime.Models;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Engines;
using System.Data.Entity;
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

        [HttpGet]
        public IActionResult StudentProfil()
        {
            if (! string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                // Get all scheduled courses...
                //var nextStudentCourse = _context.Inscrits.Where(i => i.IdEtudiant == HttpContext.Session.GetInt32("Id"));

                // SELECT * FROM etudiant e
                // INNER JOIN inscrit i
                // ON e.idEtudiant = i.idEtudiant
                // INNER JOIN cours c
                // ON c.idCours = i.idCours
                // INNER JOIN coach co
                // ON co.idCoach = c.idCoach
                // INNER JOIN discipline d
                // ON c.idDiscipline = d.idDiscipline

                var query =
                        (
                        from etudiant in _context.Etudiants.Where(e => e.IdEtudiant == HttpContext.Session.GetInt32("Id"))
                        join inscrit in _context.Inscrits
                        on etudiant.IdEtudiant equals inscrit.IdEtudiant
                        join cours in _context.Cours
                        on inscrit.IdCours equals cours.IdCours
                        join coach in _context.Coaches
                        on cours.IdCoach equals coach.IdCoach
                        join discipline in _context.Disciplines
                        on cours.IdDiscipline equals discipline.IdDiscipline
                        join salle in _context.Salles
                        on cours.IdsalleDeClasse equals salle.IdsalleDeClasse
                        join typeCours in _context.Typecours
                        on cours.IdTypeCours equals typeCours.IdTypeCours
                        select new
                        {
                            nomEtudiant = etudiant.Nom,
                            prenomEtudiant = etudiant.Prenom,
                            nomCoach = coach.Prenom,
                            nomDiscipline = discipline.Discipline1, // Cela correspond au nom de la discipline
                            limitePlace = cours.LimiteEtudiant,
                            etatDuCours = cours.Statut,
                            dateCours = cours.DateCours,
                            dureeCours = cours.Duree,
                            coursPour = cours.Pour,
                            nomTypeCours = typeCours.LibelleCours, 
                            dateInscription = inscrit.DateInscription,
                            equipement = discipline.Equipement,
                            etatInscription = inscrit.StudentStatus,
                            nomSalle = salle.Nom,
                            capaciteSalle = salle.Capacite
                        }
                    ).ToList();

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