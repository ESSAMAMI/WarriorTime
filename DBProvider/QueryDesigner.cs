using MartialTime.Controllers;
using MartialTime.Models;
using MartialTime.Models.Form;
using System;

namespace MartialTime.DBProvider
{
    public class QueryDesigner
    {

        public static Etudiant? Login(WarriortimeContext _context, SignInForm signIn)
        {
            try
            {
                return _context.Etudiants.Where(e => e.Email.Equals(signIn.email) && e.Mdp.Equals(signIn.password)).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format("Something went wrrong [{0}]", ex.Message));
            }
            
        }

        public static Object? NextActivites(WarriortimeContext _context, int id){
            try
            {
                var query = (
                        from etudiant in _context.Etudiants.Where(e => e.IdEtudiant == id)
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
                        where cours.DateCours >= DateOnly.FromDateTime(DateTime.UtcNow.Date)
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
                return query;
            }
            catch(Exception ex)
            {
                throw new Exception(String.Format("Something went wrrong [{0}]", ex.Message));
            }
        }
        public static Object? PreviousActivites(WarriortimeContext _context, int id)
        {
            try
            {
                var query = (
                        from etudiant in _context.Etudiants.Where(e => e.IdEtudiant == id)
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
                        where cours.DateCours < DateOnly.FromDateTime(DateTime.UtcNow.Date)
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
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Something went wrrong [{0}]", ex.Message));
            }
        }
    }
}
