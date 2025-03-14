using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieActorApp.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        
        [StringLength(50)]
        public string? Genre { get; set; }

       
        [Range(0, 10)]
        public double? Rating { get; set; }

        [StringLength(30)]
        public string? Director { get; set; }
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    }
}
