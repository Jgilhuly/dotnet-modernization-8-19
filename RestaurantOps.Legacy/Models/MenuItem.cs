using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOps.Legacy.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Navigation (for view convenience only, not EF)
        [NotMapped]
        public string? CategoryName { get; set; }
    }
} 