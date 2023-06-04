using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RhythmBack.Model.Models
{
    [Table("Cancion")]
    public class Cancion : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public int? Visitas { get; set; }
        public TimeSpan? Duracion { get; set; }
        public string? Lyrics { get; set; }
        [ForeignKey("CancionId")]
        public ICollection<Album>? Albums { get; set; }
        [ForeignKey("CancionId")]
        public ICollection<Artista>? Artistas { get; set; }
        [ForeignKey("CancionId")]
        public ICollection<Genero>? Generos { get; set; }
        [ForeignKey("CancionId")]
        public ICollection<Lista>? Listas { get; set; }
    }
}
