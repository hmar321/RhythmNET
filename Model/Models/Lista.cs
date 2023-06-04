using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RhythmBack.Model.Models
{
    [Table("Lista")]
    public class Lista : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Portada { get; set; }
        public string? Titulo { get; set; }
        public int? Visitas { get; set; }

        [ForeignKey("Creador")]
        public int CreadorId { get; set; }
        public Usuario? Creador { get; set; }
        public ICollection<Cancion>? Canciones { get; set; }
        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
