namespace RhythmBack.Model.DTO
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Nick { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public RolDTO? Rol { get; set; }
        public ICollection<ListaDTO>? ListasCreadas { get; set; }
        public ICollection<ListaDTO>? Listas { get; set; }
        public ICollection<AlbumDTO>? Albums { get; set; }
        public ICollection<ArtistaDTO>? Artistas { get; set; }
    }
}
