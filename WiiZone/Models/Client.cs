using System.ComponentModel.DataAnnotations;
using WiiZoneNowy.Validators; 

namespace WiiZoneNowy.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 200 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format (e.g. example@mail.com)")]
        [UniqueEmail(ErrorMessage = "Email is already used")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+?[0-9]{11}$", ErrorMessage = "Phone number must be 11 digits, optionally starting with '+'")]
        [UniquePhone(ErrorMessage = "Phone number is already used")]
        public string Phone { get; set; } = null!;
        
        public List<Reservation> Reservations { get; set; } = new();
    }
}