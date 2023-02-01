using System;
using System.Collections.Generic;

namespace MartialTime.Models
{
    public partial class Inscrit
    {
        public int IdCours { get; set; }
        public int IdEtudiant { get; set; }
        public bool? StudentStatus { get; set; }
        public DateTime DateInscription { get; set; }

        public virtual Cour IdCoursNavigation { get; set; } = null!;
        public virtual Etudiant IdEtudiantNavigation { get; set; } = null!;
    }
}
