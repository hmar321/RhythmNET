using Microsoft.EntityFrameworkCore;
using RhythmBack.Model.Models;
using System;

namespace RhythmBack.Model.Context
{
    public class RhythmDBContext : DbContext
    {
        public RhythmDBContext(DbContextOptions<RhythmDBContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
            .HasIndex(p => p.Email)
            .IsUnique();
        }


        public DbSet<Cancion>? Canciones { get; set; }
        public DbSet<Album>? Albums { get; set; }
        public DbSet<Artista>? Artistas { get; set; }
        public DbSet<Genero>? Generos { get; set; }
        public DbSet<Lista>? Listas { get; set; }
        public DbSet<Rol>? Roles { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }

    }
}