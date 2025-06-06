using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WiiZoneNowy.Validators;

namespace WiiZoneNowy.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters")]
        [UniqueGameTitle(ErrorMessage = "A game with this title already exists")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^https?:\/\/.*\.(jpg|png|gif)$", ErrorMessage = "Cover image must be a valid image URL ending with .jpg, .png, or .gif.")]
        public string CoverImage { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Genre must be between 2 and 50 characters")]
        public string Genre { get; set; }

        [Range(2006, 2020, ErrorMessage = "Release year must be between 2006 and 2020.")]
        public int ReleaseYear { get; set; }
        
        [Required]
        [Range(1, 99.99, ErrorMessage = "Price must be between 1 and 99.99")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        public bool IsReserved { get; set; } = false;
        
        [Range(0, int.MaxValue)]
        public int TimesRented { get; set; }
        
        public List<Reservation> Reservations { get; set; } = new();
        public List<GameTag> GameTags { get; set; } = new();
    }
}