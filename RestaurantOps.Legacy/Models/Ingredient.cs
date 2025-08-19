using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOps.Legacy.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Unit { get; set; } = string.Empty; // e.g. kg, pcs, l

        [Range(0, 99999)]
        public decimal QuantityOnHand { get; set; }

        [Range(0, 99999)]
        public decimal ReorderThreshold { get; set; }

        // Convenience property
        [NotMapped]
        public bool NeedsReorder => QuantityOnHand <= ReorderThreshold;
    }
} 