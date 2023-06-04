using RhythmBack.Model.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RhythmBack.Model.DTO
{
    public class AlbumDTO
    {
        public int Id { get; set; }
        public string? Portada { get; set; }
        public string? Titulo { get; set; }
        public int? Visitas { get; set; }
        public DateTime? Estreno { get; set; }
        public string? Artistas { get; set; }
        public ICollection<CancionDTO>? Canciones { get; set; }
    }
}
