using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiiZoneNowy.Models
{
    public class Reservation : IValidatableObject
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        
        public bool IsReturned { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate > EndDate)
            {
                yield return new ValidationResult(
                    "Start date must be earlier than end date.",
                    new[] { nameof(StartDate), nameof(EndDate) }
                );
            }

            if (ReservationId == 0 && StartDate < DateTime.Today)
            {
                yield return new ValidationResult(
                    "Start date must be today or later when creating a new reservation.",
                    new[] { nameof(StartDate) }
                );
            }
        }

    }
}