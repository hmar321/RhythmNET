namespace RhythmBack.Model.DTO
{
    public class ArtistaDTO
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Portada { get; set; }
        public int? Visitas { get; set; }
        public ICollection<AlbumDTO>? Albums { get; set; }
    }
}
