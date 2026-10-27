using System.ComponentModel.DataAnnotations;

namespace BookNest.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(100)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required")]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [StringLength(30)]
        public string? ISBN { get; set; }

        [Range(0, 10000)]
        public int Quantity { get; set; } = 1;

        [Range(0, 10000)]
        public int AvailableQuantity { get; set; } = 1;
    }
}