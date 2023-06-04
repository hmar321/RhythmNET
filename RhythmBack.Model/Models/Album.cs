using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RhythmBack.Model.Models
{
    [Table("Album")]
    public class Album : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Portada { get; set; }
        public string? Titulo { get; set; }
        public int? Visitas { get; set; }
        public DateTime? Estreno { get; set; }
        [ForeignKey("AlbumId")]
        public ICollection<Cancion>? Canciones { get; set; }
        [ForeignKey("AlbumId")]
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
