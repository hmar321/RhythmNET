using AutoMapper;
using RhythmBack.Model.DTO;
using RhythmBack.Model.Models;

namespace RhythmBack.Model.Util
{
    public class AutoMapperConfig
    {
        public IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile<Album, AlbumDTO>>();
                cfg.AddProfile<MappingProfile<AlbumDTO, Album>>();
                cfg.AddProfile<MappingProfile<Artista, ArtistaDTO>>();
                cfg.AddProfile<MappingProfile<ArtistaDTO, Artista>>();
                cfg.AddProfile<MappingProfile<Cancion, CancionDTO>>();
                cfg.AddProfile<MappingProfile<CancionDTO, Cancion>>();
                cfg.AddProfile<MappingProfile<Genero, GeneroDTO>>();
                cfg.AddProfile<MappingProfile<GeneroDTO, Genero>>();
                cfg.AddProfile<MappingProfile<Lista, ListaDTO>>();
                cfg.AddProfile<MappingProfile<ListaDTO, Lista>>();
                cfg.AddProfile<MappingProfile<Rol, RolDTO>>();
                cfg.AddProfile<MappingProfile<RolDTO, Rol>>();
                cfg.AddProfile<MappingProfile<Usuario, UsuarioDTO>>();
                cfg.AddProfile<MappingProfile<UsuarioDTO, Usuario>>();
            });

            return config.CreateMapper();
        }
    }
}
