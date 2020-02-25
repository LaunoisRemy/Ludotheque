using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Range(1, 200)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Prix")]
        public decimal Price { get; set; }

        [Display(Name = "Date de sortie")]
        public DateTime ReleaseDate { get; set; }

        [Url]
        [Display(Name = "Lien d'achat")]
        public string BuyLink { get; set; }

        [Url]
        [Display(Name = "Lien vidéo de présentation")]
        public string VideoLink { get; set; }

        [Display(Name = "Lien, chemin de l'image")]
        public string PictureLink { get; set; }

        [Display(Name = "Validée")]
        public bool Validate { get; set; }

        // Foreign key

        [Display(Name = "Difficulté")]
        public Difficulty Difficulty { get; set; }

        [Display(Name = "Illustrateur")]
        public Illustrator Illustrator { get; set; }

        [Display(Name = "Editeur")]
        public Editor Editor { get; set; }
        
        [Display(Name = "Categories")]
        public ICollection<Category> Categories { get; set; }



    }
}
