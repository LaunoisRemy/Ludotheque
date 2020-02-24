using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ludotheque.Models
{
    public class Jeu
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Nom { get; set; }

        public string Description { get; set; }

        [Range(1, 30)]
        [Required]
        [Display(Name = "Nombre de joueur minimum")]
        public int NbMinJoueur { get; set; }

        [Range(1, 30)]
        [Required]
        [Display(Name = "Nombre de joueur maximum")]
        public int NbMaxJoueur { get; set; }

        [Range(1, 20)]
        [Required]
        [Display(Name = "Age minimum pour jouer")]
        public int AgeMinimum { get; set; }

        [Display(Name = "Temps de jeu moyen")]
        public string TempsJeu { get; set; }

        [Range(1, 200)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Prix { get; set; }

        [Display(Name = "Lien achat du jeu")]
        public string LienAchat { get; set; }

        [Display(Name = "Lien vidéo de présentation du jeu")]
        public string LienVideo { get; set; }

        [Display(Name = "Lien, chemin image du du jeu")]
        public string LienImg { get; set; }

        [Required]
        public string Categorie { get; set; }
    }
}
