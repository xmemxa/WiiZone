using System.ComponentModel.DataAnnotations;
using WiiZoneNowy.Validators;

namespace WiiZoneNowy.Models
{
    public class Tag
    {
        [Key] public int TagId { get; set; }

        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, ErrorMessage = "Tag name must be under 50 characters")]
        [UniqueTagName]
        public string Name { get; set; } = default!;

        public List<GameTag> GameTags { get; set; } = new();
    }
}