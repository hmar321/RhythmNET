namespace RhythmBack.Model.DTO
{
    public class ListaDTO
    {
        public int Id { get; set; }
        public string? Portada { get; set; }
        public string? Titulo { get; set; }
        public int? Visitas { get; set; }
        public string? CreadorNick { get; set; }
        public int? CreadorId { get; set; }
        public ICollection<CancionDTO>? Canciones { get; set; }
    }
}
