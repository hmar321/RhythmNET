namespace RhythmBack.Model.DTO
{
    public class GeneroDTO
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public int? Visitas { get; set; }
        public string? Portada { get; set; }
        public ICollection<CancionDTO>? Canciones { get; set; }
    }
}
