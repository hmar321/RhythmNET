namespace RhythmBack.Model.DTO
{
    public class CancionDTO
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Portada { get; set; }
        public int? Visitas { get; set; }
        public TimeSpan? Duracion { get; set; }
        public DateTime Estreno { get; set; }
        public string? Lyrics { get; set; }
        public string? Artistas { get; set; }
        public bool? EnFavorito { get; set; }
    }
}
