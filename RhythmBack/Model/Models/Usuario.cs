using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RhythmBack.Model.Models
{
    [Table("Usuario")]
    public class Usuario : IEntidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Nick { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Rol? Rol { get; set; }
        [InverseProperty("Creador")]
        public ICollection<Lista>? ListasCreadas { get; set; }
        [ForeignKey("UsuarioId")]
        public ICollection<Lista>? Listas { get; set; }
        [ForeignKey("UsuarioId")]
        public ICollection<Album>? Albums { get; set; }
        [ForeignKey("UsuarioId")]
        public ICollection<Artista>? Artistas { get; set; }

    }

}
