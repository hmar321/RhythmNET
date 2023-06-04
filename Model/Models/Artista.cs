using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RhythmBack.Model.Models
{
    [Table("Artista")]
    public class Artista : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Portada { get; set; }
        public int? Visitas { get; set; }
        [ForeignKey("ArtistaId")]
        public ICollection<Cancion>? Canciones { get; set; }
        [ForeignKey("ArtistaId")]
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
