using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Ludotheque.Models
{
    public class Game
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 2)]
        [Required]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(1, 30)]
        [Required]
        [Display(Name = "Nombre de joueur minimum")]
        public int MinPlayer { get; set; }

        [Range(1, 30)]
        [Required]
        [Display(Name = "Nombre de joueur maximum")]
        public int MaxPlayer { get; set; }

        [Range(1, 20)]
        [Required]
        [Display(Name = "Age minimum")]
        public int MinimumAge { get; set; }

        [Display(Name = "Temps de jeu")]
        public string GameTime { get; set; }

        [Range(0, 200)]
        [DataType(DataType.Currency)]
        [Display(Name = "Prix")]
        public decimal Price { get; set; }

        //TODO:Check comment faire pour mettre l'année actuelle en in Range
        [Range(1900, 2100)]
        [Display(Name = "Date de sortie")]
        public int ReleaseDate { get; set; }

        [Url]
        [Display(Name = "Lien d'achat")]
        public string BuyLink { get; set; }

        [Url]
        [Display(Name = "Lien vidéo de présentation")]
        public string VideoLink { get; set; }

        [Display(Name = "Lien, chemin de l'image")]
        public string PictureLink { get; set; }

        [Display(Name = "Validé")]
        public bool Validate { get; set; }

        // Foreign key

        [Display(Name = "Difficulté")]
        [AllowNull]
        public Difficulty? Difficulty { get; set; }
        [AllowNull]
        public int? DifficultyId { get; set; }

        [Display(Name = "Illustrateur")]
        [AllowNull]
        public Illustrator? Illustrator { get; set; }
        [AllowNull]
        public int? IllustratorId { get; set; }


        [Display(Name = "Editeur")]
        [AllowNull]
        public Editor? Editor { get; set; }
        [AllowNull]
        public int? EditorId { get; set; }



    }
}
