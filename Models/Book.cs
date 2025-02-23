using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(255, ErrorMessage = "Title cannot be longer than 255 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(255, ErrorMessage = "Author name cannot be longer than 255 characters.")]
        public string Author { get; set; }

        [Range(1450, 2100, ErrorMessage = "Publication year must be between 1450 and 2100.")]
        public int PublicationYear { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Views must be a non-negative number.")]
        public int Views { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
