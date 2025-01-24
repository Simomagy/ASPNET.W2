using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace _04_LIbreria.Models
{
    public class LibroDto
    {
        [Required]
        [SwaggerSchema("The ID of the book", Format = "int32")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [SwaggerSchema("The title of the book (max length: 100)")]
        public string Titolo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [SwaggerSchema("The author of the book (max length: 100)")]
        public string Autore { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [SwaggerSchema("The genre of the book (max length: 50)")]
        public string Genere { get; set; } = string.Empty;

        [Required]
        [Range(0.01, 1000.00)]
        [SwaggerSchema("The price of the book (range: 0.01 to 1000.00)")]
        public double Prezzo { get; set; }
    }
}
