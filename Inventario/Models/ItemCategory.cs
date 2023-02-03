using System.ComponentModel.DataAnnotations;

namespace Inventario.Models
{
    public class ItemCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        public string Name { get; set; }
    }
}