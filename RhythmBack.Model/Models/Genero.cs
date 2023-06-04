using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RhythmBack.Model.Models
{
    [Table("Genero")]
    public class Genero : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public int? Visitas { get; set; }
        public string? Portada { get; set; }
        
        [ForeignKey("GeneroId")]
        public ICollection<Cancion>? Canciones { get; set; }
    }
}
